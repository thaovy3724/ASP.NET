using StoreApp.Core.Entities;

namespace StoreApp.Application.Ports.Output
{
    public interface IInventoryRepository : IBaseRepository<Inventory>
    {
        Task<Inventory> GetByProductID(Guid productID);
        Task<Inventory> deductQuantityOfCreatedOrder(Guid productID, int quantityChange);
        Task<int> GetLowStockCount();
    }
}
