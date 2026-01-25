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
    internal class ImportDetailConfiguration : IEntityTypeConfiguration<ImportDetail>
    {
        public void Configure(EntityTypeBuilder<ImportDetail> builder)
        {
            // Tên bảng
            builder.ToTable("import_details");

            // Khóa chính (Id từ BaseEntity map vào import_detail_id)
            builder.HasKey(id => id.Id);
            builder.Property(id => id.Id)
                   .HasColumnName("import_detail_id");

            // Khóa ngoại trỏ tới bảng Import
            builder.Property(id => id.ImportId)
                   .HasColumnName("import_id")
                   .IsRequired();

            // Khóa ngoại trỏ tới bảng Product
            builder.Property(id => id.ProductId)
                   .HasColumnName("product_id")
                   .IsRequired();

            // Số lượng (Quantity)
            builder.Property(id => id.Quantity)
                   .HasColumnName("quantity")
                   .IsRequired();

            // Đơn giá nhập (Price)
            builder.Property(id => id.Price)
                   .HasColumnName("price")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Thành tiền (Subtotal)
            // Vì đây là thuộc tính tính toán (computed), ta có thể cấu hình nó 
            // được tính trực tiếp bởi Database để tăng tốc độ truy vấn.
            builder.Property(id => id.Subtotal)
                   .HasColumnName("subtotal")
                   .HasColumnType("decimal(18,2)")
                   .HasComputedColumnSql("[quantity] * [price]");

            // --- Thiết lập Quan hệ ---

            // Quan hệ với bảng Product
            builder.HasOne<Product>() // Nếu bạn không có thuộc tính Navigation Product trong class
                   .WithMany()
                   .HasForeignKey(id => id.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Index để tăng tốc độ tìm kiếm sản phẩm trong các phiếu nhập
            builder.HasIndex(id => id.ImportId);
            builder.HasIndex(id => id.ProductId);

            // Validation: Số lượng và giá phải lớn hơn 0
            builder.ToTable(t => t.HasCheckConstraint("CK_ImportDetail_Quantity", "quantity > 0"));
            builder.ToTable(t => t.HasCheckConstraint("CK_ImportDetail_Price", "price >= 0"));
        }
    }
}
