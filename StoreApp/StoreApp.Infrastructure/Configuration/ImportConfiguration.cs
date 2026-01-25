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
    internal class ImportConfiguration : IEntityTypeConfiguration<Import>
    {
        public void Configure(EntityTypeBuilder<Import> builder)
        {
            // Tên bảng
            builder.ToTable("imports");

            // Khóa chính (Map Id từ BaseEntity vào import_id)
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                   .HasColumnName("import_id");

            // Ngày nhập kho
            builder.Property(i => i.ImportDate)
                   .HasColumnName("import_date")
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Foreign Key: SupplierId (Guid)
            builder.Property(i => i.SupplierId)
                   .HasColumnName("supplier_id")
                   .IsRequired();

            // Foreign Key: UserId (Guid)
            builder.Property(i => i.UserId)
                   .HasColumnName("user_id")
                   .IsRequired();

            // Tổng tiền (Tính toán nhưng vẫn lưu vào DB)
            builder.Property(i => i.TotalAmount)
                   .HasColumnName("total_amount")
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            // --- Thiết lập Quan hệ (Navigation Properties) ---

            //// Quan hệ với Nhân viên (Staff)
            //builder.HasOne(i => i.Staff)
            //       .WithMany() // Một nhân viên có thể lập nhiều phiếu nhập
            //       .HasForeignKey(i => i.UserId)
            //       .OnDelete(DeleteBehavior.Restrict); // Tránh xóa nhân viên làm mất lịch sử phiếu nhập

            // Quan hệ với Nhà cung cấp (Supplier)
            //builder.HasOne(i => i.Supplier)
            //       .WithMany()
            //       .HasForeignKey(i => i.SupplierId)
            //       .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ 1-N với ImportDetail
            builder.HasMany(i => i.ImportDetails)
                   .WithOne() // ImportDetail sẽ trỏ về Import này
                   .HasForeignKey("import_id") // Khóa ngoại bên bảng ImportDetail
                   .OnDelete(DeleteBehavior.Cascade); // Xóa phiếu nhập thì xóa luôn chi tiết
        }
    }
}
