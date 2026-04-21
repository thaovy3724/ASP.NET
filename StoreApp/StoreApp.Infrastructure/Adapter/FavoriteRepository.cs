using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class FavoriteRepository(StoreDbContext context)
        : BaseRepository<Favorite>(context), IFavoriteRepository
    {
        public Task<bool> HasFavorite(Guid customerId, Guid productId)
        {
            return DbSet.AsNoTracking()
                .AnyAsync(x => x.CustomerId == customerId && x.ProductId == productId);
        }

        public Task<Favorite?> GetByCustomerAndProduct(Guid customerId, Guid productId)
        {
            return DbSet
                .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.ProductId == productId);
        }

        public async Task<PagedList<Favorite>> Search(Guid customerId, int pageNumber, int pageSize)
        {
            var query = DbSet.AsNoTracking()
                .Include(x => x.Product)
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.CreatedAt);

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}