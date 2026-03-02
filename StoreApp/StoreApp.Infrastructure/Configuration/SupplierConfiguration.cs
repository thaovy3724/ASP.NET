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
            builder.ToTable("supplier");
            builder.HasKey(s => s.Id);

            // 1. Id
            builder.Property(s => s.Id)
                   .HasColumnName("supplier_id");

            // 2. Name 
            builder.Property(s => s.Name)
                   .HasColumnType("nvarchar(100)")
                   .IsRequired();

            // 3. Phone 
            builder.Property(s => s.Phone)
                   .HasColumnType("nvarchar(10)")
                   .IsRequired();

            // 4. Email
            builder.Property(s => s.Email)
                   .HasColumnType("nvarchar(255)")
                   .IsRequired();

            // 5. Address
            builder.Property(s => s.Address)
                   .HasColumnType("nvarchar(500)") // 
                   .IsRequired();
        }
    }
}
