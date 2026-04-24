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

            builder.Property(o => o.CustomerId)
                   .HasColumnName("customer_id")
                   .IsRequired();

            // Nhân viên bán hàng
            builder.Property(o => o.StaffId)
                   .HasColumnName("staff_id");

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
            builder.Property(o => o.TotalAmount)
                   .HasColumnName("total_amount")
                   .HasColumnType("decimal(18,2)");
            builder.Property(o => o.VoucherCode)
                   .HasColumnName("voucher_code");

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
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
