using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.Repository
{
    public interface IGRNRepository : IBaseRepository<GRN>
    {
        Task<List<GRN>> Search(Guid? supplierId = null, GRNStatus? status = null);
    }
}
