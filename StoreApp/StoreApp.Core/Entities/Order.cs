using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class Order : BaseEntity
    {
        public Guid? CustomerId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? PromoId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;
        public decimal DiscountAmount { get; private set; } = 0;
        public decimal TotalAmount { get; private set; } = 0;
        public List<OrderItem> Items { get; private set; } = [];
    }
}
