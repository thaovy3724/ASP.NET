using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class ProductRepository(StoreDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        // Create
        // Trong hệ thống đã có sản phẩm nào dùng barcode này chưa?
        public async Task<bool> ExistsBarcode(string barcode)
        {
            // AnyAsync trả về bool, không load entity như FirstAsync => chạy nhanh hơn 
            return await DbSet.AnyAsync(x => x.Barcode == barcode);
        }

        // Update
        // Ngoài chính sản phẩm đang sửa, còn sản phẩm nào khác dùng barcode này không? 
        public async Task<bool> ExistsBarcodeForOtherProducts(Guid id, string barcode)
        {
            // AnyAsync trả về bool, không load entity như FirstAsync => chạy nhanh hơn 
            return await DbSet.AnyAsync(x => x.Id != id && x.Barcode == barcode);
        }

        // filter 
        public async Task<List<Product>> Search(Guid? supplierId, Guid? categoryId, decimal? minPrice, decimal? maxPrice, string? keyword, string? priceOrder)
        {
            IQueryable<Product> query = DbSet.AsNoTracking();

            if (supplierId.HasValue)
                query = query.Where(x => x.SupplierId == supplierId.Value);

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            if (minPrice.HasValue)
                query = query.Where(x => x.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(x => x.Price <= maxPrice.Value);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var k = keyword.Trim();
                query = query.Where(x =>
                    x.ProductName.Contains(k) ||
                    x.Barcode.Contains(k)
                );
            }

            if (!string.IsNullOrWhiteSpace(priceOrder))
            {
                query = priceOrder.Trim().ToLower() switch
                {
                    "asc" => query.OrderBy(x => x.Price),
                    "desc" => query.OrderByDescending(x => x.Price),
                    _ => query      // nếu giá trị khác asc và desc thì không làm gì 
                };
            }

            return await query.ToListAsync();
        }
    }
}