namespace StoreApp.Core.Entities
{
    public class CartItem(Guid cartId, Guid productId, int quantity) : BaseEntity
    {
        public Guid CartId { get; private set; } = cartId;
        public Guid ProductId { get; private set; } = productId;
        public int Quantity { get; private set; } = quantity;

        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}