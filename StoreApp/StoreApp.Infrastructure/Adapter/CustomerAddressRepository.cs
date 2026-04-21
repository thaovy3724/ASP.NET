using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class CustomerAddressRepository(StoreDbContext context) : BaseRepository<CustomerAddress>(context), ICustomerAddressRepository
    {
        public Task<List<CustomerAddress>> GetByCustomerIdAsync(Guid customerId)
        {
            return DbSet.AsNoTracking()
                        .Where(x => x.CustomerId == customerId)
                        .OrderByDescending(x => x.IsDefault)
                        .ThenByDescending(x => x.CreatedAt)
                        .ToListAsync();
        }

        public Task<CustomerAddress?> GetByIdAndCustomerIdAsync(Guid id, Guid customerId)
        {
            return DbSet.FirstOrDefaultAsync(x => x.Id == id && x.CustomerId == customerId);
        }

        public Task<CustomerAddress?> GetDefaultByCustomerIdAsync(Guid customerId)
        {
            return DbSet.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.IsDefault);
        }

        public Task<bool> HasAnyByCustomerIdAsync(Guid customerId)
        {
            return DbSet.AsNoTracking().AnyAsync(x => x.CustomerId == customerId);
        }

        public async Task ResetDefaultAsync(Guid customerId)
        {
            await DbSet.Where(x => x.CustomerId == customerId && x.IsDefault)
                       .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.IsDefault, false));
        }
    }
}
