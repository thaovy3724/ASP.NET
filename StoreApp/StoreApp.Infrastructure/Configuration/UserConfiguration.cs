using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            // Khóa chính
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                   .HasColumnName("user_id");

            // Username: Cần Unique để không bị trùng tên đăng nhập
            builder.Property(u => u.Username)
                   .HasColumnName("username")
                   .HasColumnType("nvarchar(255)")
                   .IsRequired();

            // Password: Thường để dài hơn để lưu Hash
            builder.Property(u => u.Password)
                   .HasColumnName("password")
                   .HasColumnType("nvarchar(255)")
                   .IsRequired();

            // Số điện thoại (Varchar 20)
            builder.Property(c => c.Phone)
                   .HasColumnName("phone")
                   .HasColumnType("nvarchar(10)");

            // FullName: Sử dụng nvarchar để hỗ trợ tiếng Việt có dấu
            builder.Property(u => u.FullName)
                   .HasColumnName("full_name")
                   .HasColumnType("nvarchar(100)")
                   .IsRequired();

            // Role
            builder.Property(u => u.Role)
                    .HasColumnName("role")
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

            // CreatedAt
            builder.Property(u => u.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(u => u.RefreshToken)
                   .HasColumnName("refresh_token")
                   .HasColumnType("nvarchar(255)");

            // RefreshTokenExpiryTime
            builder.Property(u => u.RefreshTokenExpiryTime)
                   .HasColumnName("refresh_token_expiry_time")
                   .HasColumnType("datetime");
            builder.Property(u => u.failedLoginCount)
                    .HasColumnName("failed_login_count")
                    .HasColumnType("int").HasDefaultValue(0);

            builder.Property(u => u.lockoutEnd)
                    .HasColumnName("lock_out_end")
                    .HasColumnType("int").HasDefaultValue(0);

            builder.Property(u => u.lockedDate)
                    .HasColumnName("locked_date")
                    .HasColumnType("datetime");

            // --- INDEXING ---
            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.RefreshToken);
        }
    }
}
