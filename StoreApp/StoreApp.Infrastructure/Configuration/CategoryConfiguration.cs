using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");
            // Khóa chính
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasColumnName("category_id");

            builder.Property(c => c.Name)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
