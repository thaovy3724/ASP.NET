using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<PagedList<Product>> Search(
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
        );

        Task<bool> DecreaseStockIfAvailable(Guid productId, int quantity);
    }
}