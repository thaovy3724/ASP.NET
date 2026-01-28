using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class OrderRepository(StoreDbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
    }
}
