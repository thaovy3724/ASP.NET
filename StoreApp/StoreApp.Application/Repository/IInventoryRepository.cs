using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IInventoryRepository : IBaseRepository<Inventory>
    {
        Task<Inventory?> GetByProductID(Guid productID);
        //Task<Inventory> deductQuantityOfCreatedOrder(Guid productID, int quantityChange);
        Task<int> GetLowStockCount();
    }
}
