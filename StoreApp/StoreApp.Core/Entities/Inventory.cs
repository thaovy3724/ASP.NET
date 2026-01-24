namespace StoreApp.Core.Entities
{
    public class Inventory : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;
    }
}
