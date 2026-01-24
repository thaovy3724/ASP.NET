namespace StoreApp.Core.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; private set; } = "";
        public string Phone { get; private set; } = "";
        public string Email { get; private set; } = "";
        public string Address { get; private set; } = "";
    }
}
