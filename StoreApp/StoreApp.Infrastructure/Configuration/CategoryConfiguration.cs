using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category");
            // Khóa chính
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasColumnName("category_id");

            builder.Property(c => c.Name)
                   .HasColumnName("name")
                   .HasColumnType("nvarchar(100)")
                   .IsRequired();

            builder.HasIndex(c => c.Name).IsUnique(); // Đảm bảo tên danh mục là duy nhất
        }
    }
}
