using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Configuration
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("promotions");

            //builder.HasKey(p => p.PromoId);
            //builder.Property(p => p.PromoId).HasColumnName("promo_id");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                   .HasColumnName("promo_id");


            // Validation: PromoCode là bắt buộc, tối đa 50 ký tự và KHÔNG ĐƯỢC TRÙNG
            builder.Property(p => p.PromoCode)
                   .HasColumnName("promo_code")
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.HasIndex(p => p.PromoCode).IsUnique();

            builder.Property(p => p.Description)
                   .HasColumnName("description")
                   .HasColumnType("varchar(255)");

            // Validation: DiscountType chỉ nhận 'percent' hoặc 'fixed'
            builder.Property(p => p.DiscountType)
                   .HasColumnName("discount_type")
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            // Validation: Giá trị giảm giá không được âm
            builder.Property(p => p.DiscountValue)
                   .HasColumnName("discount_value")
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            // Validation: Định dạng ngày tháng
            builder.Property(p => p.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date")
                    .IsRequired();

            builder.Property(p => p.EndDate)
                   .HasColumnName("end_date")
                   .HasColumnType("date")
                   .IsRequired();
        }
    }
}
