using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    // khai báo để mô tả nhu cầu truy vấn
    public interface ICategoryRepository : IBaseRepository<Category>    // base đã có create/update/delete...
    {
        Task<List<Category>> Search(string? keyword = null);
    }
}
