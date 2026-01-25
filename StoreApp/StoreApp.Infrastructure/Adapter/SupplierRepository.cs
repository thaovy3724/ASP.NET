using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public class SupplierRepository(DbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
    {
    }
}
