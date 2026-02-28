using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class GRNRepository(StoreDbContext context) : BaseRepository<GRN>(context), IGRNRepository
    {
        public async Task<List<GRN>> Search(Guid? supplierId = null, GRNStatus? status = null)
        {
            var query = DbSet.AsNoTracking();

            if(supplierId is not null)
                query = query.Where(x => x.SupplierId == supplierId);

            if(status is not null)
                query = query.Where(x => x.Status == status);

            return await query.ToListAsync();
        }
    }
}
