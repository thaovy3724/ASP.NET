namespace StoreApp.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal Subtotal => Quantity * Price;
    }
}
