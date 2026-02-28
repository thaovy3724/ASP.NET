using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Tên bảng
            builder.ToTable("order");

            // Khóa chính (Map từ BaseEntity)
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                   .HasColumnName("order_id");

            // Ngày cập nhật trạng thái đơn hàng lần gần nhất 
            builder.Property(o => o.UpdatedAt)
                   .HasColumnName("updated_date")
                   .HasColumnType("datetime")
                   .IsRequired();

            // Khách hàng (Có thể null nếu khách lẻ không đăng ký)
            builder.Property(o => o.CustomerId)
                   .HasColumnName("customer_id");

            // Nhân viên bán hàng (Bắt buộc)
            builder.Property(o => o.StaffId)
                   .HasColumnName("staff_id")
                   .IsRequired();

            // OrderStatus
            builder.Property(u => u.OrderStatus)
                    .HasColumnName("order_status")
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

            // PaymentMethod
            builder.Property(u => u.PaymentMethod)
                    .HasColumnName("payment_method")
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

            // Address
            builder.Property(o => o.Address)
                   .HasColumnName("address")
                   .HasColumnType("nvarchar(500)")
                   .IsRequired();

            // --- Thiết lập Quan hệ ---

            // Quan hệ với Khách hàng
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ với Nhân viên (Staff/User)
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(o => o.StaffId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ 1-N với OrderItem
            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey("order_id")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
