using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.ToTable("product_review");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasColumnName("product_review_id");

            builder.Property(x => x.ProductId)
                   .HasColumnName("product_id")
                   .IsRequired();

            builder.Property(x => x.CustomerId)
                   .HasColumnName("customer_id")
                   .IsRequired();

            builder.Property(x => x.Rating)
                   .HasColumnName("rating")
                   .IsRequired();

            builder.Property(x => x.Comment)
                   .HasColumnName("comment")
                   .HasColumnType("nvarchar(500)")
                   .HasMaxLength(500);

            builder.Property(x => x.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                   .WithMany()
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.CustomerId, x.ProductId })
                   .IsUnique();
        }
    }
}