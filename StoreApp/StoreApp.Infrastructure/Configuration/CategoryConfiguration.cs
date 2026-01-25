using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CategoryName)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
