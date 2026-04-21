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
        public int FailedLoginCount { get; private set; } = 0;
        public DateTime? LockoutEnd { get; private set; }
        public void Update(string username, string fullName, string phone, Role role)
        {
            Username = username;
            FullName = fullName;
            Phone = phone;
            Role = role;
        }

        public bool IsTemporarilyLocked(DateTime now)
        {
            return LockoutEnd.HasValue && LockoutEnd.Value > now;
        }

        public void ClearExpiredLockout(DateTime now)
        {
            if (LockoutEnd.HasValue && LockoutEnd.Value <= now)
            {
                FailedLoginCount = 0;
                LockoutEnd = null;
            }
        }

        public void RegisterFailedLogin(DateTime now, int maxFailedAttempts, TimeSpan lockoutDuration)
        {
            FailedLoginCount++;

            if (FailedLoginCount >= maxFailedAttempts)
            {
                LockoutEnd = now.Add(lockoutDuration);
            }
        }

        public void ResetLoginFailure()
        {
            FailedLoginCount = 0;
            LockoutEnd = null;
        }
    }
}
