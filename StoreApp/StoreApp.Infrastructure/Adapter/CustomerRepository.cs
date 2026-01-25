using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public class CustomerRepository(DbContext context) : BaseRepository<Customer>(context), ICustomerRepository 
    {
    }
}
