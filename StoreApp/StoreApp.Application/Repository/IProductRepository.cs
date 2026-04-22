using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        // filter 
        Task<PagedList<Product>> Search(
            int pageNumber, 
            int pageSize, 
            Guid? categoryId = null,
            Guid? supplierId = null,
            decimal? minPrice = null, 
            decimal? maxPrice = null, 
            string? keyword = null);

        Task<bool> DecreaseStockIfAvailable(Guid productId, int quantity);
        Task DeleteListOfProduct(List<Guid> productIds);
    }
}
