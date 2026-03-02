namespace StoreApp.Core.Entities
{
    public class Supplier(string name, string phone, string email, string address) : BaseEntity
    {
        public string Name { get; private set; } = name;
        public string Phone { get; private set; } = phone;
        public string Email { get; private set; } = email;
        public string Address { get; private set; } = address;
        //private Supplier() : this(default!, default!, default!, default!) { }
        public void Update(string name, string phone, string email, string address)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }
    }
}
