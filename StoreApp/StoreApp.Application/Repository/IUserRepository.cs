using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> SearchByKeyword(string keyword);
        Task<bool> IsUsernameExist(string username);
        Task<bool> IsUserExist(Guid userId);
        Task<bool> IsExistUserOfOrder(Guid userId);
        Task<User?> GetByUserName(string username);
        Task<User?> GetByUserId(Guid userId);

    }
}
