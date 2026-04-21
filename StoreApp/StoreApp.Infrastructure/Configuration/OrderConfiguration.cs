using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                   .HasColumnName("order_id");

            builder.Property(o => o.UpdatedAt)
                   .HasColumnName("updated_date")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(o => o.CustomerId)
                   .HasColumnName("customer_id")
                   .IsRequired();

            builder.Property(o => o.StaffId)
                   .HasColumnName("staff_id");

            builder.Property(o => o.AddressId)
                   .HasColumnName("address_id");

            builder.Property(u => u.OrderStatus)
                   .HasColumnName("order_status")
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(u => u.PaymentMethod)
                   .HasColumnName("payment_method")
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            // Lưu snapshot địa chỉ giao hàng để lịch sử Order không bị đổi khi Customer sửa/xóa địa chỉ.
            builder.Property(o => o.Address)
                   .HasColumnName("address")
                   .HasColumnType("nvarchar(500)")
                   .IsRequired();

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(o => o.StaffId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<CustomerAddress>()
                   .WithMany()
                   .HasForeignKey(o => o.AddressId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
