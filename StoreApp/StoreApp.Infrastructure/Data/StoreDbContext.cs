using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Data
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
    {
        //public DbSet<User> Users { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Supplier> Suppliers { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Inventory> Inventories { get; set; }
        //public DbSet<Promotion> Promotions { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        //public DbSet<Import> Imports { get; set; }
        //public DbSet<ImportDetail> ImportDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dòng này sẽ tự động tìm tất cả các class thực thi IEntityTypeConfiguration 
            // trong cùng Assembly (project Infrastructure) và áp dụng chúng.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }
    }
}
