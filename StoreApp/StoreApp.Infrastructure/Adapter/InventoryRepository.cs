using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public class InventoryRepository(DbContext context) : BaseRepository<Inventory>(context), IInventoryRepository
    {
    }
}
