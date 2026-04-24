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
    internal class VourcherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("voucher");
            // Khóa chính
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
                   .HasColumnName("code");
            builder.Property(v => v.DiscountPercent)
                   .HasColumnName("discount_percent")
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();
            builder.Property(v => v.MaxDiscountAmount)
                   .HasColumnName("max_discount_amount")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            builder.Property(v => v.StartDate)
                   .HasColumnName("start_date")
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(v => v.EndDate)
                   .HasColumnName("end_date")
                   .HasColumnType("datetime")
                   .IsRequired();
            builder.Property(v => v.Quantity)
                   .HasColumnName("quantity")
                   .IsRequired();
            builder.Property(v => v.Status)
                   .HasColumnName("status")
                   .IsRequired();
        }
    }
}
