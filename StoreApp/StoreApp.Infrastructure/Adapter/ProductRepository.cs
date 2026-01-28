using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class ProductRepository(StoreDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        private readonly DbSet<Product> _dbset = context.Set<Product>();
        public async Task<bool> checkExistBarcode(string barcode)
        {
            return await _dbset
                .AnyAsync(x => x.Barcode == barcode);

        }

        public async Task<bool> checkExistID(Guid ID)
        {
            bool exists = await _dbset
                .AnyAsync(x => x.Id == ID);

            return exists;
        }
        public async Task<bool> checkBarcodeExistForOtherProducts(Guid id, string barcode)
        {
            var sanpham = await GetById(id);
            var exist = await _dbset.AnyAsync(x => x.Barcode == sanpham.Barcode && x.Id != sanpham.Id);
            return exist;
        }
        public async Task<List<Product>> getProductsBysupplierIDAndCategoryIDAndPriceAndKeyword(Guid? supplier_id, Guid? category_id, string? order, string? keyword)
        {
            try
            {

                var query = _dbset.AsQueryable();

                // Filter by supplier
                if (supplier_id.HasValue)
                {
                    query = query.Where(p => p.SupplierId == supplier_id.Value);
                }

                // Filter by category
                if (category_id.HasValue)
                {
                    query = query.Where(p => p.CategoryId == category_id.Value);
                }

                // Search by keyword
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(x => x.ProductName.ToLower().Contains(keyword.ToLower()) && x.Barcode.ToLower().Contains(keyword.ToLower()));
                }

                // Sort by price
                if (!string.IsNullOrEmpty(order))
                {
                    query = order.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Price)
                        : query.OrderBy(p => p.Price);
                }

                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi khi lấy danh sách sản phẩm theo yêu cầu, nhà cung cấp, loại, keyword: " + e.Message);
            }
        }

    }
}
