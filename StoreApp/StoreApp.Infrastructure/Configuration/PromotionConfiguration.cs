using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Configuration
{
    internal class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("promotions");

            //builder.HasKey(p => p.PromoId);
            //builder.Property(p => p.PromoId).HasColumnName("promo_id");
            builder.HasKey(s => s.Id);


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
                   .HasColumnType("varchar(20)") // Hoặc .HasConversion<string>() nếu dùng Enum
                   .IsRequired();

            // Validation: Giá trị giảm giá không được âm
            builder.Property(p => p.DiscountValue)
                   .HasColumnName("discount_value")
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            // Validation: Định dạng ngày tháng
            builder.Property(p => p.StartDate).HasColumnName("start_date").HasColumnType("date").IsRequired();
            builder.Property(p => p.EndDate).HasColumnName("end_date").HasColumnType("date").IsRequired();

            builder.Property(p => p.MinOrderAmount).HasColumnName("min_order_amount").HasColumnType("decimal(10,2)");
            builder.Property(p => p.UsageLimit).HasColumnName("usage_limit");
            builder.Property(p => p.UsedCount).HasColumnName("used_count");
            builder.Property(p => p.PromotionStatus).HasColumnName("status").HasColumnType("enum('active','inactive')");

            // --- THÊM CÁC RÀNG BUỘC (CHECK CONSTRAINTS) ---

            // 1. Ngày kết thúc phải sau ngày bắt đầu
            builder.ToTable(t => t.HasCheckConstraint("CK_Promo_Dates", "end_date >= start_date"));

            // 2. Giá trị giảm giá phải lớn hơn 0
            builder.ToTable(t => t.HasCheckConstraint("CK_Promo_DiscountValue", "discount_value > 0"));

            // 3. UsedCount không được vượt quá UsageLimit (nếu có giới hạn)
            builder.ToTable(t => t.HasCheckConstraint("CK_Promo_Usage", "used_count <= usage_limit OR usage_limit = 0"));
        }
    }
}
