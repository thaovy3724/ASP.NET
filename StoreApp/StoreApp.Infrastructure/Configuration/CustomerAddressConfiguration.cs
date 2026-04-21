using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Configuration
{
    internal class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.ToTable("customer_address");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .HasColumnName("customer_address_id");

            builder.Property(x => x.CustomerId)
                   .HasColumnName("customer_id")
                   .IsRequired();

            builder.Property(x => x.ReceiverName)
                   .HasColumnName("receiver_name")
                   .HasColumnType("nvarchar(100)")
                   .IsRequired();

            builder.Property(x => x.Phone)
                   .HasColumnName("phone")
                   .HasColumnType("nvarchar(20)")
                   .IsRequired();

            builder.Property(x => x.AddressLine)
                   .HasColumnName("address_line")
                   .HasColumnType("nvarchar(500)")
                   .IsRequired();

            builder.Property(x => x.IsDefault)
                   .HasColumnName("is_default")
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(x => x.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.CustomerId);

            // SQL Server filtered unique index: mỗi customer chỉ có 1 địa chỉ mặc định
            builder.HasIndex(x => new { x.CustomerId, x.IsDefault })
                   .IsUnique()
                   .HasFilter("[is_default] = 1");
        }
    }
}
