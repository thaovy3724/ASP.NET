using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            // Tên bảng
            builder.ToTable("product");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasColumnName("product_id");

            // Cấu hình các cột (Property)
            builder.Property(p => p.ProductName)
                   .HasColumnName("product_name")
                   .HasColumnType("nvarchar(100)")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Price)
                   .HasColumnName("price")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(p => p.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(p => p.Quantity)
                   .HasColumnName("quantity")
                   .IsRequired();

            builder.Property(p => p.ImageUrl)
                   .HasColumnName("image_url")
                   .HasColumnType("nvarchar(500)")
                   .IsRequired();

            // Cấu hình Quan hệ (Relationships)
            builder.HasOne<Supplier>()
                   .WithMany()
                   .HasForeignKey(o => o.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(o => o.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
