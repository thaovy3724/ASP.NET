using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class VoucherRepository(StoreDbContext context)
        : BaseRepository<Voucher>(context), IVoucherRepository
    {
        public async Task<PagedList<Voucher>> Search(
            int pageNumber,
            int pageSize,
            string? keyword = null)
        {
            var query = DbSet.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim().ToUpper();
                query = query.Where(x => x.Code.Contains(k));
            }

            query = query.OrderByDescending(x => x.EndDate);

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<Voucher?> GetByCode(string code)
        {
            var normalized = code.Trim().ToUpper();

            return await DbSet
                .FirstOrDefaultAsync(x => x.Code == normalized);
        }

        public async Task<bool> IsCodeExist(string code, Guid? exceptId = null)
        {
            var normalized = code.Trim().ToUpper();

            return await DbSet.AsNoTracking()
                .AnyAsync(x => x.Code == normalized && (exceptId == null || x.Id != exceptId));
        }

        // Sử dụng ExecuteUpdateAsync để giảm số lượng voucher một cách an toàn trong môi trường có nhiều người dùng đồng thời
        public async Task<bool> TryDecreaseQuantity(Guid voucherId, DateTime now)
        {
            var affected = await DbSet
                .Where(x => x.Id == voucherId
                            && x.Status == VoucherStatus.Active
                            && x.Quantity > 0
                            && x.StartDate <= now
                            && x.EndDate >= now)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(x => x.Quantity, x => x.Quantity - 1));

            return affected > 0;
        }

        public async Task<List<Voucher>> GetExpiredActiveVouchers(DateTime now)
        {
            return await DbSet
                .Where(x => x.Status == VoucherStatus.Active && x.EndDate < now)
                .ToListAsync();
        }

        public async Task<List<Voucher>> GetAvailableVouchers(DateTime now)
        {
            return await DbSet.AsNoTracking()
                .Where(x => x.Status == VoucherStatus.Active
                            && x.Quantity > 0
                            && x.StartDate <= now
                            && x.EndDate >= now)
                .OrderBy(x => x.EndDate)
                .ToListAsync();
        }
    }
}