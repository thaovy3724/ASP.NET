using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class OrderRepository(StoreDbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public async Task<PagedList<Order>> Search(int pageNumber, int pageSize, Guid? customerId = null)
        {
            // 1. Dùng AsNoTracking để tăng tốc độ (vì đây là lệnh Query, không phải Update)
            // 2. Include các bảng liên quan để DTO có đủ dữ liệu
            var query = DbSet.AsNoTracking()
                             .Include(o => o.Items)
                             .AsQueryable();

            if (customerId is not null)
                query = query.Where(x => x.CustomerId == customerId);

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        // Override method GetById ở BaseRepository 
        public new Task<Order?> GetById(Guid id)
        {
            return DbSet.AsNoTracking()
                        .Include(o => o.Items)
                        .FirstOrDefaultAsync(o => o.Id == id);
        }

        public Task<List<Order>> GetListOrderWithDetails()
        {
            return DbSet.AsNoTracking()
                        .Include(o => o.Items)
                        .ToListAsync();
        }

        public Task<List<Order>> GetListExpiredOrders(DateTime timeLimit)
        { // kiểm tra các đơn hàng Pending và chưa quá hạn
            return DbSet.AsNoTracking()
                        .Where(o => o.OrderStatus == OrderStatus.Pending && o.UpdatedAt < timeLimit)
                        .ToListAsync();
        }

        public Task<bool> HasProductReference(Guid productId)
        {
            return DbSet.AsNoTracking()
                        .Include(o => o.Items)
                        .AnyAsync(o => o.Items.Any(oi => oi.ProductId == productId));
        }
    }
}
