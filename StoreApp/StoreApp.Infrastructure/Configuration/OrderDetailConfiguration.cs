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
    internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Tên bảng
            builder.ToTable("order_details");
            // Khóa chính (Id từ BaseEntity map vào order_detail_id)
            builder.HasKey(od => od.Id);
            builder.Property(od => od.Id)
                   .HasColumnName("order_detail_id");
            // Khóa ngoại trỏ tới bảng Order
            builder.Property(od => od.OrderId)
                   .HasColumnName("order_id")
                   .IsRequired();
            // Khóa ngoại trỏ tới bảng Product
            builder.Property(od => od.ProductId)
                   .HasColumnName("product_id")
                   .IsRequired();
            // Số lượng (Quantity)
            builder.Property(od => od.Quantity)
                   .HasColumnName("quantity")
                   .IsRequired();
            // Đơn giá bán (Price)
            builder.Property(od => od.Price)
                   .HasColumnName("price")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            // Thành tiền (Subtotal)
            // Vì đây là thuộc tính tính toán (computed), ta có thể cấu hình nó 
            // được tính trực tiếp bởi Database để tăng tốc độ truy vấn.
            builder.Property(od => od.Subtotal)
                   .HasColumnName("subtotal")
                   .HasColumnType("decimal(18,2)")
                   .HasComputedColumnSql("[quantity] * [price]");
            // --- Thiết lập Quan hệ ---
            // Quan hệ với bảng Product
            builder.HasOne<Product>() // Nếu bạn không có thuộc tính Navigation Product trong class
                   .WithMany()
                   .HasForeignKey(od => od.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
            // Quan hệ với bảng Order
            builder.HasOne<Order>() // Nếu bạn không có thuộc tính Navigation Order trong class
                   .WithMany()
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Cascade); // Xóa chi tiết khi đơn hàng bị xóa
        }
    {
    }
}
