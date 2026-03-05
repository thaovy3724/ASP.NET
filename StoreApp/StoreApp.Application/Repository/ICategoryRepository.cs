using StoreApp.Application.Repository;
using StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    // khai báo để mô tả nhu cầu truy vấn
    public interface ICategoryRepository : IBaseRepository<Category>    // base đã có create/update/delete...
    {
        Task<PagedList<Category>> Search(int pageNumber, int pageSize, string? keyword = null);
    }
}
