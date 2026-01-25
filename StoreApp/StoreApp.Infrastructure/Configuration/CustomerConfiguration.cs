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
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Tên bảng
            builder.ToTable("customers");

            // Khóa chính
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasColumnName("customer_id");

            // Tên khách hàng (Bắt buộc, varchar 100)
            builder.Property(c => c.Name)
                   .HasColumnName("name")
                   .HasColumnType("varchar(100)")
                   .IsRequired();

            // Số điện thoại (Varchar 20)
            builder.Property(c => c.Phone)
                   .HasColumnName("phone")
                   .HasColumnType("varchar(20)");

            // Email (Varchar 100)
            builder.Property(c => c.Email)
                   .HasColumnName("email")
                   .HasColumnType("varchar(100)");

            // Địa chỉ (Mặc định nvarchar max nếu không chỉ định TypeName)
            builder.Property(c => c.Address)
                   .HasColumnName("address");

            // Ngày tạo
            builder.Property(c => c.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()");

            // Điểm thưởng
            // Lưu ý: Nếu trong Entity bạn đang comment nó, hãy mở ra để builder có thể nhận diện
            //builder.Property(c => c.RewardPoints)
            //       .HasColumnName("reward_points")
            //       .HasColumnType("int")
            //       .HasDefaultValue(0);

            // --- Cấu hình bổ sung (Best Practice) ---

            // Index cho số điện thoại và email để tìm kiếm khách hàng nhanh hơn
            builder.HasIndex(c => c.Phone);
            builder.HasIndex(c => c.Email).IsUnique(); // Email thường không nên trùng nhau
        }
    }
}
