namespace StoreApp.Core.Entities
{
    public class Order : BaseEntity
    {
        public Guid? CustomerId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid? PromoId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal DiscountAmount { get; private set; } = 0;
        public decimal TotalAmount => Items?.Sum(i => i.Subtotal) ?? 0 - DiscountAmount;
        public List<OrderItem> Items { get; private set; } = [];
        //public List<Payment> Payments { get; private set; } = new();
    }
}
