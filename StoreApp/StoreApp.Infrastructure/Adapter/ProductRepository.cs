using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class ProductRepository(StoreDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        // filter 
        public async Task<PagedList<Product>> Search(int pageNumber, int pageSize, Guid? categoryId = null, Guid? supplierId = null, String? orderDate = null, decimal? minPrice = null, decimal? maxPrice = null, string? keyword = null)
        {
            var query = DbSet.AsNoTracking();

            if (categoryId is not null)
                query = query.Where(x => x.CategoryId == categoryId.Value);
            if(supplierId is not null)
                query = query.Where(x => x.SupplierId == supplierId.Value);
            if (orderDate is not null)
            {
                if(orderDate.StartsWith("asc", StringComparison.OrdinalIgnoreCase))
                    query = query.OrderBy(x => x.CreatedAt);
                else 
                    query = query.OrderByDescending(x => x.CreatedAt);
            }

            if (minPrice is not null)
                query = query.Where(x => x.Price >= minPrice.Value);

            if (maxPrice is not null)
                query = query.Where(x => x.Price <= maxPrice.Value);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim();
                query = query.Where(x =>
                    x.ProductName.Contains(k)
                );
            }
            // mới nhất hiện trước 
            //query = query.OrderByDescending(x => x.CreatedAt);
            return await query.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}