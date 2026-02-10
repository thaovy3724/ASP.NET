using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class InventoryRepository(StoreDbContext context) : BaseRepository<Inventory>(context), IInventoryRepository
    {
        public async Task<Inventory?> GetByProductID(Guid productID)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.ProductId == productID);
        }

        // chưa dùng Order
        public async Task<Inventory?> deductQuantityOfCreatedOrder(Guid productID, int quantityChange)
        {
            var inv = await DbSet.FirstOrDefaultAsync(x => x.ProductId == productID);
            if (inv is null) return null;

            inv.UpdateQuantity(inv.Quantity - quantityChange);
            DbSet.Update(inv);
            await context.SaveChangesAsync();
            return inv;
        }

        public Task<int> GetLowStockCount()
        {
            const int lowStockThreshold = 10;   // đại đại đi 
            return DbSet.CountAsync(x => x.Quantity < lowStockThreshold);
        }

        public Task RestockQuantity(Guid productID, int quantityChange)
        {
            var inv = DbSet.FirstOrDefaultAsync(x => x.ProductId == productID);
            if (inv is null) return Task.CompletedTask;
            inv.Result.UpdateQuantity(inv.Result.Quantity + quantityChange);
            DbSet.Update(inv.Result);
            return context.SaveChangesAsync();
        }
    }
}
