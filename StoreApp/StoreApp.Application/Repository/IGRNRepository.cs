using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.Repository
{
    public interface IGRNRepository : IBaseRepository<GRN>
    {
        Task<PagedList<GRN>> Search(int pageNumber, int pageSize, Guid? supplierId = null, GRNStatus? status = null);
        Task<GRN?> GetByIdWithItems(Guid id);
        void MarkDetailAsAdded(GRNDetail item);

        // kiểm tra xem có tham chiếu đến product nào trước khi xóa không 
        // dùng cho product repository
        Task<bool> HasProductReference(Guid productId);
    }
}
