using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public class OrderRepository(DbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
    }
}
