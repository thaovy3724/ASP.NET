namespace StoreApp.Core.Entities
{
    public class GRNDetail(Guid grnId, Guid productId, int quantity, decimal price) : BaseEntity
    {
        public Guid GRNId { get; private set; } = grnId;
        public Guid ProductId { get; private set; } = productId;
        public int Quantity { get; private set; } = quantity;
        public decimal Price { get; private set; } = price;
    }
}
