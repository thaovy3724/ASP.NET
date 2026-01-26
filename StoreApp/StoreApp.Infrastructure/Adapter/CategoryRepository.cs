using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    // chuyển output port từ Application thành query EF Core
    // hoàn toàn không chứa nghiệp vụ.
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context) { }

        public async Task<List<Category>> SearchByName(string? keyword)
        {
            keyword = keyword?.Trim();

            IQueryable<Category> query = DbSet.AsNoTracking();

            // keyword rỗng => lấy tất cả
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                // có keywords (lọc theo tên)
                query = query.Where(x => EF.Functions.Like(x.CategoryName, $"%{keyword}%"));
            }

            // sắp xếp theo tên 
            return await query
                .OrderBy(x => x.CategoryName)
                .ToListAsync();
        }

        public async Task<Category?> GetByExactName(string categoryName)
        {
            var name = categoryName?.Trim();
            if (string.IsNullOrWhiteSpace(name)) return null;

            return await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CategoryName == name);
        }
    }
}
