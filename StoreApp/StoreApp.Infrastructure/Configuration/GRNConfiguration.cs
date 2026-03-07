using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class GRNConfiguration : IEntityTypeConfiguration<GRN>
    {
        public void Configure(EntityTypeBuilder<GRN> builder)
        {
            builder.ToTable("GRN");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                   .HasColumnName("GRN_id");

            builder.Property(i => i.SupplierId)
                   .HasColumnName("supplier_id")
                   .IsRequired();

            builder.Property(i => i.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasColumnType("datetime")
                   .IsRequired();

            // GRNStatus
            builder.Property(u => u.Status)
                    .HasColumnName("GRN_status")
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

            builder.HasOne<Supplier>()
                   .WithMany()
                   .HasForeignKey(i => i.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ 1-N với GRNDetail
            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey(od => od.GRNId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
