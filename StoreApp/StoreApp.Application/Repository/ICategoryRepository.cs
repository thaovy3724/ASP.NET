using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    // khai báo để mô tả nhu cầu truy vấn
    public interface ICategoryRepository : IBaseRepository<Category>    // base đã có create/update/delete...
    {
        // Tìm kiếm/lọc theo tên (keyword rỗng => trả về tất cả).
        Task<List<Category>> Search(string? keyword);

        // Lấy category theo đúng tên (để kiểm tra trùng).
        Task<Category?> GetByExactName(string categoryName);

        Task<Category?> GetByExactName(string categoryName, Guid excludeId);
    }
}
