using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class ProductReviewRepository(StoreDbContext context)
        : BaseRepository<ProductReview>(context), IProductReviewRepository
    {
        public Task<bool> HasCustomerReviewed(Guid customerId, Guid productId)
        {
            return DbSet.AsNoTracking()
                .AnyAsync(x => x.CustomerId == customerId && x.ProductId == productId);
        }

        public Task<bool> HasCustomerPurchasedProduct(Guid customerId, Guid productId)
        {
            return context.Set<Order>()
                .AsNoTracking()
                .Include(o => o.Items)
                .AnyAsync(o =>
                    o.CustomerId == customerId &&
                    (o.OrderStatus == OrderStatus.Delivered || o.OrderStatus == OrderStatus.Paid) &&
                    o.Items.Any(i => i.ProductId == productId));
        }

        public Task<List<ProductReview>> GetByProductId(Guid productId)
        {
            return DbSet.AsNoTracking()
                .Include(x => x.Customer)
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<(double AverageRating, int ReviewCount)> GetSummary(Guid productId)
        {
            var query = DbSet.AsNoTracking()
                .Where(x => x.ProductId == productId);

            var count = await query.CountAsync();

            if (count == 0)
                return (0, 0);

            var average = await query.AverageAsync(x => x.Rating);

            return (Math.Round(average, 1), count);
        }
    }
}