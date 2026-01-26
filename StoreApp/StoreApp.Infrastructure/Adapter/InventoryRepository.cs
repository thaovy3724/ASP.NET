using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public class InventoryRepository(DbContext context) : BaseRepository<Inventory>(context), IInventoryRepository
    {
        private readonly DbSet<Inventory> _dbset = context.Set<Inventory>();
        public async Task<Inventory> GetByProductID(Guid productID)
        {
            try
            {
                return await _dbset.FirstOrDefaultAsync(x => x.Id == productID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Inventory> UpdateInventory(Guid productID, int quantityChange)
        {
            var tonkho = await _dbset.FirstOrDefaultAsync(x => x.Id == productID);
            if (tonkho == null)
            {
                throw new Exception("Không tìm thấy tồn kho cho sản phẩm với ID: " + productID);
            }
            tonkho.AdjustQuantity(quantityChange);
            tonkho.UpdateTimestamp();
            _dbset.Update(tonkho);
            await context.SaveChangesAsync();
            return tonkho;
        }

        public async Task<Inventory> deductQuantityOfCreatedOrder(Guid productID, int quantityChange)
        {
            var tonkho = await _dbset.FirstOrDefaultAsync(x => x.Id == productID);
            if (tonkho == null)
            {
                return null;
            }
            tonkho.AdjustQuantity(-quantityChange);
            tonkho.UpdateTimestamp();
            _dbset.Update(tonkho);
            await context.SaveChangesAsync();
            return tonkho;
        }

        public Task<int> GetLowStockCount()
        {
            int lowStockThreshold = 10; // Ngưỡng tồn kho thấp
            return _dbset.CountAsync(tk => tk.Quantity < lowStockThreshold);
        }
    }
}
