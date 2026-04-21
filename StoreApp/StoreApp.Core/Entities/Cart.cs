namespace StoreApp.Core.Entities
{
    public class Cart(Guid customerId) : BaseEntity
    {
        public Guid CustomerId { get; private set; } = customerId;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.AddHours(7);
        public List<CartItem> Items { get; private set; } = [];

        public void AddItem(Guid productId, int quantity)
        {
            var existingItem = Items.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem is not null)
            {
                existingItem.UpdateQuantity(existingItem.Quantity + quantity);
                return;
            }

            Items.Add(new CartItem(Id, productId, quantity));
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}