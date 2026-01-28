using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Tên bảng
            builder.ToTable("orders");

            // Khóa chính (Map từ BaseEntity)
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                   .HasColumnName("order_id");

            // Ngày đặt hàng
            builder.Property(o => o.OrderDate)
                   .HasColumnName("order_date")
                   .HasColumnType("datetime")
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            // Khách hàng (Có thể null nếu khách lẻ không đăng ký)
            builder.Property(o => o.CustomerId)
                   .HasColumnName("customer_id");

            // Nhân viên bán hàng (Bắt buộc)
            builder.Property(o => o.UserId)
                   .HasColumnName("user_id")
                   .IsRequired();

            // Mã giảm giá (Có thể null)
            builder.Property(o => o.PromoId)
                   .HasColumnName("promo_id");

            // Số tiền giảm giá
            builder.Property(o => o.DiscountAmount)
                   .HasColumnName("discount_amount")
                   .HasColumnType("decimal(10,2)")
                   .HasDefaultValue(0);

            // Tổng tiền đơn hàng
            builder.Property(o => o.TotalAmount)
                   .HasColumnName("total_amount")
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();
            // OrderStatus
            builder.Property(u => u.OrderStatus)
                    .HasColumnName("order_status")
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

            // --- Thiết lập Quan hệ ---

            // Quan hệ với Khách hàng
            builder.HasOne<Customer>()
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Quan hệ với Nhân viên (Staff/User)
            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ với Mã giảm giá
            builder.HasOne<Promotion>()
                   .WithMany()
                   .HasForeignKey(o => o.PromoId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Quan hệ 1-N với OrderItem
            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey("order_id")
                   .OnDelete(DeleteBehavior.Cascade);

            //// --- Tối ưu hóa ---

            //// Index để tìm kiếm đơn hàng theo ngày hoặc theo khách hàng nhanh hơn
            //builder.HasIndex(o => o.OrderDate);
            //builder.HasIndex(o => o.CustomerId);

            //// Check Constraint: Đảm bảo tổng tiền không âm
            //builder.ToTable(t => t.HasCheckConstraint("CK_Order_TotalAmount", "total_amount >= 0"));
        }
    }
}
