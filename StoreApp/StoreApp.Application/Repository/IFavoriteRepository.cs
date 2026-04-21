using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IFavoriteRepository : IBaseRepository<Favorite>
    {
        Task<bool> HasFavorite(Guid customerId, Guid productId);

        Task<Favorite?> GetByCustomerAndProduct(Guid customerId, Guid productId);

        Task<PagedList<Favorite>> Search(Guid customerId, int pageNumber, int pageSize);
    }
}