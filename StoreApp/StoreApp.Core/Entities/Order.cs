using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class Order(
        Guid? customerId,
        Guid userId,
        Guid? promoId,
        DateTime orderDate,
        OrderStatus orderStatus,
        decimal discountAmount) : BaseEntity
    {
        public Guid? CustomerId { get; private set; } = customerId;
        public Guid UserId { get; private set; } = userId;
        public Guid? PromoId { get; private set; } = promoId;
        public DateTime OrderDate { get; private set; } = orderDate;
        public OrderStatus OrderStatus { get; private set; } = orderStatus;
        public decimal DiscountAmount { get; private set; } = discountAmount;
        public decimal TotalAmount { get; private set; }

        // Đảm bảo list không bị null
        public List<OrderItem> Items { get; private set; } = [];

        public void Update( OrderStatus status)
        {
            OrderStatus = status;
        }

        public void SetDiscount(decimal amount)
        {
            if (amount < 0) amount = 0;
            DiscountAmount = amount;
        }

        public void CalculateTotal()
        {
            var subTotal = Items.Sum(x => x.Subtotal);
            TotalAmount = subTotal - DiscountAmount;
            if (TotalAmount < 0) TotalAmount = 0;
        }

    }
}