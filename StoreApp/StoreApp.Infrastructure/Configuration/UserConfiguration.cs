using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            // Khóa chính
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("user_id");

            // Username: Cần Unique để không bị trùng tên đăng nhập
            builder.Property(u => u.Username)
                   .HasColumnName("username")
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.HasIndex(u => u.Username).IsUnique();

            // Password: Thường để dài hơn để lưu Hash
            builder.Property(u => u.Password)
                   .HasColumnName("password")
                   .HasColumnType("varchar(255)")
                   .IsRequired();

            // FullName: Sử dụng nvarchar để hỗ trợ tiếng Việt có dấu
            builder.Property(u => u.FullName)
                   .HasColumnName("full_name")
                   .HasColumnType("nvarchar(100)")
                   .IsRequired();

            // Role
            builder.Property(u => u.Role)
                   .HasColumnName("role")
                   .HasColumnType("varchar(20)")
                   .IsRequired()
                   .HasDefaultValue("staff");

            // CreatedAt
            builder.Property(u => u.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()"); // Hoặc CURRENT_TIMESTAMP tùy DB

            //// Status
            //builder.Property(u => u.Status)
            //       .HasColumnName("status")
            //       .HasColumnType("tinyint")
            //       .HasDefaultValue(1);
        }
    }
}
