using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        Task<PagedList<Supplier>> Search(int pageNumber, int pageSize, string? keyword = null);
    }
}
