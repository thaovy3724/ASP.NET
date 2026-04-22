using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class User(string username, string password, string fullName, string phone, Role role, DateTime createdAt) : BaseEntity
    {
        public string Username { get; private set; } = username;
        public string Password { get; private set; } = password;
        public string FullName { get; private set; } = fullName;
        public string Phone { get; private set; } = phone;
        public Role Role { get; private set; } = role;
        public DateTime CreatedAt { get; private set; } = createdAt;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // Số lần đăng nhập sai liên tiếp
        public int FailedLoginCount { get; private set; } = 0;

        // Thời điểm hết khóa đăng nhập
        public DateTime? LockoutEnd { get; private set; }

        public void Update(string username, string fullName, string phone, Role role)
        {
            Username = username;
            FullName = fullName;
            Phone = phone;
            Role = role;
        }

        public bool IsLoginLocked(DateTime now)
        {
            return LockoutEnd.HasValue && LockoutEnd.Value > now;
        }

        public bool IsLockoutExpired(DateTime now)
        {
            return LockoutEnd.HasValue && LockoutEnd.Value <= now;
        }

        public void IncreaseFailedLogin(int maxFailedCount, TimeSpan lockoutDuration, DateTime now)
        {
            FailedLoginCount++;

            if (FailedLoginCount >= maxFailedCount)
            {
                LockoutEnd = now.Add(lockoutDuration);
            }
        }

        public void ResetFailedLogin()
        {
            FailedLoginCount = 0;
            LockoutEnd = null;
        }
    }
}