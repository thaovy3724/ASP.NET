using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class ProductRepository(StoreDbContext context)
        : BaseRepository<Product>(context), IProductRepository
    {
        public async Task<bool> DecreaseStockIfAvailable(Guid productId, int quantity)
        {
            var rowEffected = await DbSet
                .Where(p => p.Id == productId && p.Quantity >= quantity)
                .ExecuteUpdateAsync(s => s.SetProperty(
                    p => p.Quantity,
                    p => p.Quantity - quantity
                ));

            return rowEffected > 0;
        }

        public async Task<PagedList<Product>> Search(
            int pageNumber,
            int pageSize,
            Guid? categoryId = null,
            Guid? supplierId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? minQuantity = null,
            int? maxQuantity = null,
            string? keyword = null,
            string? sortBy = null,
            bool isDescending = true
        )
        {
            // AsNoTracking dùng cho truy vấn chỉ đọc, giúp nhẹ hơn vì EF không cần tracking entity
            IQueryable<Product> query = DbSet.AsNoTracking();

            if (categoryId is not null)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            if (supplierId is not null)
                query = query.Where(x => x.SupplierId == supplierId.Value);

            if (minPrice is not null)
                query = query.Where(x => x.Price >= minPrice.Value);

            if (maxPrice is not null)
                query = query.Where(x => x.Price <= maxPrice.Value);

            if (minQuantity is not null)
                query = query.Where(x => x.Quantity >= minQuantity.Value);

            if (maxQuantity is not null)
                query = query.Where(x => x.Quantity <= maxQuantity.Value);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim();

                query = query.Where(x =>
                    x.ProductName.Contains(k)
                );
            }

            query = ApplySorting(query, sortBy, isDescending);

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        private static IQueryable<Product> ApplySorting(
            IQueryable<Product> query,
            string? sortBy,
            bool isDescending
        )
        {
            var sort = string.IsNullOrWhiteSpace(sortBy)
                ? "CreatedAt"
                : sortBy.Trim();

            return sort.ToLowerInvariant() switch
            {
                "productname" => isDescending
                    ? query.OrderByDescending(x => x.ProductName)
                    : query.OrderBy(x => x.ProductName),

                "price" => isDescending
                    ? query.OrderByDescending(x => x.Price)
                    : query.OrderBy(x => x.Price),

                "quantity" => isDescending
                    ? query.OrderByDescending(x => x.Quantity)
                    : query.OrderBy(x => x.Quantity),

                "createdat" => isDescending
                    ? query.OrderByDescending(x => x.CreatedAt)
                    : query.OrderBy(x => x.CreatedAt),

                _ => query.OrderByDescending(x => x.CreatedAt)
            };
        }
    }
}