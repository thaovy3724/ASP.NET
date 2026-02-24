using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        // filter 
        Task<List<Product>> Search(Guid? categoryId = null, decimal? minPrice = null, decimal? maxPrice = null, string? keyword = null);
    }
}
