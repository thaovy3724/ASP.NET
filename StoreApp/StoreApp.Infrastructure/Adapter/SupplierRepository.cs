using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class SupplierRepository(StoreDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
    {
        public async Task<PagedList<Supplier>> Search(int pageNumber, int pageSize, string? keyword = null)
        {
            var query = DbSet.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(x =>
                    (x.Name != null && x.Name.Contains(keyword)) ||
                    (x.Email != null && x.Email.Contains(keyword)) ||
                    (x.Phone != null && x.Phone.Contains(keyword)) ||
                    (x.Address != null && x.Address.Contains(keyword)));
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
