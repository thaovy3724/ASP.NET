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
    internal class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("inventory");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                   .HasColumnName("inventory_id");

            builder.Property(i => i.ProductId)
                   .HasColumnName("product_id")
                   .IsRequired();

            builder.Property(i => i.Quantity)
                   .HasColumnName("quantity")
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(i => i.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.HasOne<Product>()
                   .WithOne()
                   .HasForeignKey<Inventory>(i => i.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 1 product chỉ có 1 inventory
            builder.HasIndex(i => i.ProductId).IsUnique();

            // Thiết lập Quan hệ (Nếu Product cũng nằm trong cùng DB/Service)
            // Nếu đây là Microservice riêng biệt, bạn có thể không cần Navigation Property 
            // mà chỉ cần lưu ProductId để đảm bảo tính độc lập.

            //builder.ToTable(t => t.HasCheckConstraint("CK_Inventory_Quantity", "quantity >= 0"));
        }
    }
}
