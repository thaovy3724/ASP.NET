using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class User(string username, string password, string fullName, string phone, string email, Role role, DateTime createdAt) : BaseEntity
    {
        // Gán trực tiếp giá trị từ tham số constructor vào thuộc tính
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
        public string FullName { get; private set; } = fullName;
        public string Phone { get; private set; } = phone;
        public string Email { get; private set; } = email;
        public Role Role { get; private set; } = role;
        public DateTime CreatedAt { get; private set; } = createdAt;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public bool IsActive { get; set; } = false;
        // BẮT BUỘC: Thêm constructor trống để EF Core có thể khởi tạo đối tượng khi lấy dữ liệu từ DB
        //private User() : this(default!, default!, default!, default!, default!) { }

        public void Update(string username, string password, string fullName, string phone, string email, Role role, bool isActive)
        {
            Username = username;
            Password = password;
            FullName = fullName;
            Phone = phone;
            Email = email;
            Role = role;
            IsActive = isActive;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}
