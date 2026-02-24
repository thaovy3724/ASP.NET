using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> Search(string? keyword = null);
    }
}
