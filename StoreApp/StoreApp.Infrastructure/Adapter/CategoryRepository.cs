using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    // chuyển output port từ Application thành query EF Core
    // hoàn toàn không chứa nghiệp vụ.
    public class CategoryRepository(StoreDbContext context) : BaseRepository<Category>(context), ICategoryRepository
    {
        public async Task<List<Category>> Search(string? keyword)
        {
            keyword = keyword?.Trim();

            IQueryable<Category> query = DbSet.AsNoTracking();

            // keyword rỗng => lấy tất cả
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                // có keywords (lọc theo tên)
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{keyword}%"));
            }

            // sắp xếp theo tên 
            return await query
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Category?> GetByExactName(string? name)
        {
            name = name?.Trim();
            if (string.IsNullOrWhiteSpace(name)) return null;

            return await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Category?> GetByExactName(string? name, Guid excludeId)
        {
            name = name?.Trim();
            if (string.IsNullOrWhiteSpace(name)) return null;

            return await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name && x.Id != excludeId);
        }
    }
}
