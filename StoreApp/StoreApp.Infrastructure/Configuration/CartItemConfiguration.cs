using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_item");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                   .HasColumnName("cart_item_id");

            builder.Property(i => i.CartId)
                   .HasColumnName("cart_id")
                   .IsRequired();

            builder.Property(i => i.ProductId)
                   .HasColumnName("product_id")
                   .IsRequired();

            builder.Property(i => i.Quantity)
                   .HasColumnName("quantity")
                   .IsRequired();

            builder.HasOne<Product>()
                   .WithMany()
                   .HasForeignKey(i => i.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(i => new { i.CartId, i.ProductId })
                   .IsUnique();
        }
    }
}