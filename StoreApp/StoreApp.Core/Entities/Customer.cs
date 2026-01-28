namespace StoreApp.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; private set; } = "";
        public string? Phone { get; private set; }
        public string? Email { get; private set; }
        public string? Address { get; private set; }
        public DateTime? CreatedAt { get; private set; } = DateTime.Now;
    }
}
