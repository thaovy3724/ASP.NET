using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class User(string username, string password, string fullName, string phone, Role role, DateTime createdAt) : BaseEntity
    {
        // Gán trực tiếp giá trị từ tham số constructor vào thuộc tính
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
        public string FullName { get; private set; } = fullName;
        public string Phone { get; private set; } = phone;
        public Role Role { get; private set; } = role;
        public DateTime CreatedAt { get; private set; } = createdAt;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public bool IsLocked { get; private set; } = false;
        public void Update(string username, string fullName, string phone, Role role)
        {
            Username = username;
            FullName = fullName;
            Phone = phone;
            Role = role;
        }

        public void Lock()
        {
            IsLocked = true;
        }

        public void Unlock()
        {
            IsLocked = false;
        }
    }
}
