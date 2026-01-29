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
            builder.Property(c => c.Id)
                   .HasColumnName("supplier_id");

            builder.Property(s => s.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(s => s.Phone)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(s => s.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(s => s.Address).IsRequired()
                    .HasColumnType("text");
        }
    }
}
