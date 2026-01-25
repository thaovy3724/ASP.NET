using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Configuration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            // Tên bảng
            builder.ToTable("products");
            builder.HasKey(s => s.Id);


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
                   .HasColumnType("timestamp")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Tự động lấy tgian thực nếu dùng MySQL/Postgres

            builder.Property(p => p.ImageUrl)
                   .HasColumnName("image_url")
                   .HasColumnType("varchar(500)");

            //builder.Property(p => p.Status)
            //       .HasColumnName("status")
            //       .HasColumnType("bit(1)");

            // Cấu hình Quan hệ (Relationships)
            //builder.HasOne(p => p.Category)
            //       .WithMany() // Nếu LoaiSanPham không có List<SanPham>
            //       .HasForeignKey(p => p.CategoryID)
            //       .OnDelete(DeleteBehavior.SetNull);

            //builder.HasOne(p => p.Supplier)
            //       .WithMany()
            //       .HasForeignKey(p => p.SupplierID)
            //       .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
