namespace StoreApp.Core.Entities
{
    public class GRNDetail(Guid GRNId, Guid productId, int quantity, decimal price) : BaseEntity
    {
        public Guid GRNId { get; private set; } = GRNId;
        public Guid ProductId { get; private set; } = productId;
        public int Quantity { get; private set; } = quantity;
        public decimal Price { get; private set; } = price;

        public void Update(int quantity, decimal price)
        {
            Quantity = quantity;
            Price = price;
        }
    }
}
