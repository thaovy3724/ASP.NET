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
            String? orderDate = null,
            decimal? minPrice = null, 
            decimal? maxPrice = null, 
            string? keyword = null);
    }
}
