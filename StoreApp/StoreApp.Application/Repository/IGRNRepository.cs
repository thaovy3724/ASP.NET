using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.Repository
{
    public interface IGRNRepository : IBaseRepository<GRN>
    {
        Task<PagedList<GRN>> Search(int pageNumber, int pageSize, Guid? supplierId = null, GRNStatus? status = null);
        Task<GRN?> GetByIdWithItems(Guid id);
        void MarkDetailAsAdded(GRNDetail item);
    }
}
