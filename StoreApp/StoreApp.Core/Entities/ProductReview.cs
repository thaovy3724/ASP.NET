namespace StoreApp.Core.Entities
{
    public class ProductReview(Guid productId, Guid customerId, int rating, string? comment) : BaseEntity
    {
        public Guid ProductId { get; private set; } = productId;
        public Guid CustomerId { get; private set; } = customerId;
        public int Rating { get; private set; } = rating;
        public string? Comment { get; private set; } = comment;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.AddHours(7);

        // Navigation property để lấy thông tin Product
        public Product Product { get; private set; } = null!;

        // Navigation property để lấy tên Customer
        public User Customer { get; private set; } = null!;
    }
}