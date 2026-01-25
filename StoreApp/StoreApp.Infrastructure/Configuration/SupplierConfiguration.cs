using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Configuration
{
    internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("suppliers");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(s => s.Phone).IsRequired()
                   .HasMaxLength(10);
            builder.Property(s => s.Email).IsRequired()
                   .HasMaxLength(100);
            builder.Property(s => s.Address).IsRequired()
                    .HasColumnType("text");
            //builder.HasMany(s => s.Products)
            //       .WithOne(p => p.Supplier)
            //       .HasForeignKey(p => p.SupplierId);
        }
    }
}
