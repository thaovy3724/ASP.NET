using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        // khai báo các nhu cầu truy vấn 

        // Create
        // Trong hệ thống đã có sản phẩm nào dùng barcode này chưa?
        Task<bool> ExistsBarcode(string barcode);

        // Update
        // Ngoài chính sản phẩm đang sửa, còn sản phẩm nào khác dùng barcode này không? 
        Task<bool> ExistsBarcodeForOtherProducts(Guid id, string barcode);

        // filter 
        Task<List<Product>> Search(Guid? supplierId, Guid? categoryId, decimal? minPrice, decimal? maxPrice, string? priceOrder, string? keyword);
    }
}
