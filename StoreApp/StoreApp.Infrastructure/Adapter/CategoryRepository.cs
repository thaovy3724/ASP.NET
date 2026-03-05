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
        public async Task<PagedList<Category>> Search(string? keyword = null, int pageNumber, int pageSize)
        {

            var query = DbSet.AsNoTracking();

            // keyword rỗng => lấy tất cả
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                // có keywords (lọc theo tên)
                query = query.Where(x => x.Name.Contains(keyword));
            }
            return await query.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
