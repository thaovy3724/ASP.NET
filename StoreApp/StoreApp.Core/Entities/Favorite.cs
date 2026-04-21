namespace StoreApp.Core.Entities
{
    public class Favorite(Guid customerId, Guid productId) : BaseEntity
    {
        public Guid CustomerId { get; private set; } = customerId;
        public Guid ProductId { get; private set; } = productId;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.AddHours(7);

        public User Customer { get; private set; } = null!;
        public Product Product { get; private set; } = null!;
    }
}