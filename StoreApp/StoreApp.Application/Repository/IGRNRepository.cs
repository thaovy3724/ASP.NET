using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IGRNRepository : IBaseRepository<GRN>
    {
        Task<GRN?> GetByProductID(Guid productID);
        Task<GRN> deductQuantityOfCreatedOrder(Guid productID, int quantityChange);
        Task<int> GetLowStockCount();
        Task RestockQuantity(Guid productID, int quantityChange);
    }
}
