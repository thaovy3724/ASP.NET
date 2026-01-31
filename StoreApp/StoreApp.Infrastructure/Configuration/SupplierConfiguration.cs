using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Configuration
{
    internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("suppliers");
            builder.HasKey(s => s.Id);

            // 1. Sửa lại Id cho khớp với kiểu uniqueidentifier trong DB
            builder.Property(s => s.Id)
                   .HasColumnName("supplier_id")
                   .HasColumnType("uniqueidentifier");

            // 2. Name dùng nvarchar là chuẩn
            builder.Property(s => s.Name)
                   .HasColumnType("nvarchar(100)")
                   .IsRequired();

            // 3. Phone nên để nvarchar để tránh lỗi khi có dấu cách/dấu cộng
            builder.Property(s => s.Phone)
                   .HasColumnType("nvarchar(20)")
                   .IsRequired();

            builder.Property(s => s.Email)
                   .HasColumnType("nvarchar(100)")
                   .IsRequired();

            // 4. Sửa lại Address: Bỏ .HasColumnType("text")
            builder.Property(s => s.Address)
                   .HasColumnType("nvarchar(500)") // nvarchar(500) là đủ cho địa chỉ và search rất nhanh
                   .IsRequired();
        }
    }
}
