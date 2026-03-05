using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Data
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dòng này sẽ tự động tìm tất cả các class thực thi IEntityTypeConfiguration 
            // trong cùng Assembly (project Infrastructure) và áp dụng chúng.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }
    }
}
