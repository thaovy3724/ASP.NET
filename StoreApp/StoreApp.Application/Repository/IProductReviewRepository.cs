using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IProductReviewRepository : IBaseRepository<ProductReview>
    {
        Task<bool> HasCustomerReviewed(Guid customerId, Guid productId);

        Task<bool> HasCustomerPurchasedProduct(Guid customerId, Guid productId);

        Task<List<ProductReview>> GetByProductId(Guid productId);

        Task<(double AverageRating, int ReviewCount)> GetSummary(Guid productId);
    }
}