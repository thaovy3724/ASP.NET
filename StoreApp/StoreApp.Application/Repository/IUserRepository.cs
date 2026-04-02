using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<PagedList<User>> Search(int pageNumber, int pageSize, string? keyword = null, Role? role = null);
        Task<User?> GetByName(string name);
    }
}
