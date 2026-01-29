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
    internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Tên bảng
            builder.ToTable("payments");

            // Khóa chính
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasColumnName("payment_id");

            // Khóa ngoại tới đơn hàng
            builder.Property(p => p.OrderId)
                   .HasColumnName("order_id")
                   .IsRequired();

            // Số tiền thanh toán
            builder.Property(p => p.Amount)
                   .HasColumnName("amount")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Cấu hình Enum PaymentMethod
            // Lưu dưới dạng String (Cash, Card,...) để dễ truy vấn báo cáo trong SQL
            builder.Property(p => p.PaymentMethod)
                   .HasColumnName("payment_method")
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            // Ngày thanh toán
            builder.Property(p => p.PaymentDate)
                   .HasColumnName("payment_date")
                   .HasColumnType("datetime")
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            // --- Thiết lập Quan hệ ---

            // Quan hệ N-1 với Order
            builder.HasOne<Order>()
                   .WithOne() // Một đơn hàng có thể có nhiều đợt thanh toán (ví dụ: đặt cọc và thanh toán nốt)
                   .HasForeignKey<Payment>(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade); // Xóa đơn hàng thì xóa lịch sử thanh toán liên quan

            // --- Tối ưu hóa & Validation ---

            //// Index để tra cứu lịch sử thanh toán theo đơn hàng nhanh hơn
            //builder.HasIndex(p => p.OrderId);

            //// Kiểm tra số tiền thanh toán phải lớn hơn 0
            //builder.ToTable(t => t.HasCheckConstraint("CK_Payment_Amount", "amount > 0"));

        }
    }
}
