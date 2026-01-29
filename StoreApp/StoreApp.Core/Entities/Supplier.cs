namespace StoreApp.Core.Entities
{
    public class Supplier(string name, string phone, string email, string address) : BaseEntity
    {
        public string Name { get; private set; } = "";
        public string Phone { get; private set; } = "";
        public string Email { get; private set; } = "";
        public string Address { get; private set; } = "";
        public void Update(string name, string phone, string email, string address)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}
