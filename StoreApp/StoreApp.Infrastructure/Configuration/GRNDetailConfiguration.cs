using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class GRNDetailConfiguration : IEntityTypeConfiguration<GRNDetail>
    {
        public void Configure(EntityTypeBuilder<GRNDetail> builder)
        {
            // Tên bảng
            builder.ToTable("GRN_detail");
            // Khóa chính (Id từ BaseEntity map vào GRN_detail_id)
            builder.HasKey(od => od.Id);
            builder.Property(od => od.Id)
                   .HasColumnName("GRN_detail_id");

            // Khóa ngoại trỏ tới bảng Inventory
            builder.Property(od => od.GRNId)
                   .HasColumnName("GRN_id")
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

            // --- Thiết lập Quan hệ ---
            // Quan hệ với bảng Product
            builder.HasOne<Product>() 
                   .WithMany()
                   .HasForeignKey(od => od.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(od => new { od.GRNId, od.ProductId }).IsUnique(); // Đảm bảo mỗi sản phẩm chỉ xuất hiện một lần trong cùng một GRN
        }
    }
}
