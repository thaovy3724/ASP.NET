using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("cart");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("cart_id");

            builder.Property(c => c.CustomerId)
                   .HasColumnName("customer_id")
                   .IsRequired();

            builder.Property(c => c.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(c => c.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Items)
                   .WithOne()
                   .HasForeignKey(i => i.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.CustomerId)
                   .IsUnique();
        }
    }
}