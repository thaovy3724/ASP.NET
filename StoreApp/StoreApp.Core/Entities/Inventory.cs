namespace StoreApp.Core.Entities
{
    public class Inventory(Guid productId, int quantity) : BaseEntity
    {
        public Guid ProductId { get; private set; } = productId;
        public int Quantity { get; private set; } = quantity;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;

        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
            UpdatedAt = DateTime.Now;
        }
    }
}
