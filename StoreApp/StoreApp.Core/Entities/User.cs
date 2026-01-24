using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; private set; } = "";
        public string Password { get; private set; } = "";
        public string FullName { get; private set; } = "";
        public Role Role { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
    }
}
