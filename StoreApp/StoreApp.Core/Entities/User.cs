using StoreApp.Core.ValueObject;
using System.Runtime.CompilerServices;

namespace StoreApp.Core.Entities
{
    public class User(string username, string password, string fullName, Role role, DateTime createdAt) : BaseEntity
    {
        // Gán trực tiếp giá trị từ tham số constructor vào thuộc tính
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
        public string FullName { get; private set; } = fullName;
        public Role Role { get; private set; } = role;
        public DateTime CreatedAt { get; private set; } = createdAt;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        // BẮT BUỘC: Thêm constructor trống để EF Core có thể khởi tạo đối tượng khi lấy dữ liệu từ DB
        private User() : this(default!, default!, default!, default!, default!) { }

        public void Update(string username, string password, string fullName, Role role)
        {
            Username = username;
            Password = password;
            FullName = fullName;
            Role = role;
        }
    }
}
