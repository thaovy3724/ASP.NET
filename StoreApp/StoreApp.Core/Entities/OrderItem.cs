namespace StoreApp.Core.Entities
{
    public class OrderItem(Guid orderId, Guid productId, int quantity, decimal price) : BaseEntity
    {
        public Guid OrderId { get; private set; } = orderId;
        public Guid ProductId { get; private set; } = productId;
        public int Quantity { get; private set; } = quantity;
        public decimal Price { get; private set; } = price;
        public decimal Subtotal => Quantity * Price;
    }
}
