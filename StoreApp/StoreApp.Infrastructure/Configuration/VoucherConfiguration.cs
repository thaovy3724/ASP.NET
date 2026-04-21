using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("voucher");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                   .HasColumnName("voucher_id");

            builder.Property(v => v.Code)
                   .HasColumnName("code")
                   .HasColumnType("nvarchar(50)")
                   .HasMaxLength(50)
                   .IsRequired();

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
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasIndex(v => v.Code)
                   .IsUnique();
        }
    }
}