using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            // Tên bảng
            builder.ToTable("products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasColumnName("product_id");

            // Cấu hình các cột (Property)
            builder.Property(p => p.ProductName)
                   .HasColumnName("product_name")
                   .HasColumnType("varchar(100)")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Barcode)
                   .HasColumnName("barcode")
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(p => p.Price)
                   .HasColumnName("price")
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(p => p.Unit)
                   .HasColumnName("unit")
                   .HasColumnType("varchar(20)");

            builder.Property(p => p.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.Property(p => p.ImageUrl)
                   .HasColumnName("image_url")
                   .HasColumnType("varchar(500)");

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
