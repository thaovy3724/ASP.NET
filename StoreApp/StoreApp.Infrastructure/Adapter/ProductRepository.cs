using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public class ProductRepository(DbContext context) : BaseRepository<Product>(context), IProductRepository
    {
    }
}
