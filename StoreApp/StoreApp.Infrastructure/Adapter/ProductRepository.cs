using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class ProductRepository(StoreDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        public async Task<bool> DecreaseStockIfAvailable(Guid productId, int quantity)
        {
            var rowEffected = await DbSet
                .Where(p => p.Id == productId && !p.IsDeleted && p.Quantity >= quantity)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.Quantity, p => p.Quantity - quantity));
            return rowEffected > 0;
        }

        // filter 
        public async Task<PagedList<Product>> Search(int pageNumber, int pageSize, Guid? categoryId = null, Guid? supplierId = null, decimal? minPrice = null, decimal? maxPrice = null, string? keyword = null, bool isDeleted = false)
        {
            var query = DbSet.AsNoTracking().Where(x => x.IsDeleted == isDeleted);

            if (categoryId is not null)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            if (supplierId is not null)
                query = query.Where(x => x.SupplierId == supplierId.Value);

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
            query = query.OrderByDescending(x => x.CreatedAt);
            return await query.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}