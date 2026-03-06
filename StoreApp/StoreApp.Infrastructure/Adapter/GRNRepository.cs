using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class GRNRepository(StoreDbContext context) : BaseRepository<GRN>(context), IGRNRepository
    {
        public async Task<PagedList<GRN>> Search(int pageNumber, int pageSize, Guid? supplierId = null, GRNStatus? status = null)
        {
            var query = DbSet.AsNoTracking()
                        .Include(o => o.Items)
                        .AsQueryable();

            if (supplierId is not null)
                query = query.Where(x => x.SupplierId == supplierId);

            if(status is not null)
                query = query.Where(x => x.Status == status);

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        // Override method GetById ở BaseRepository 
        public new Task<GRN?> GetById(Guid id)
        {
            return DbSet.AsNoTracking()
                        .Include(o => o.Items)
                        .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
