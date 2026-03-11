using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<PagedList<User>> Search(int pageNumber, int pageSize, string? keyword = null);
        Task<User?> GetByName(string name);
        Task<User?> GetByEmail(string email);
    }
}
