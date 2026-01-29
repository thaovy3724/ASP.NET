using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> SearchByKeyword(string keyword);
        Task<bool> isUsernameExist(string username);
        Task<bool> isUserExist(Guid userId);
    }
}
