using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        List<User> SearchByKeyword(string keyword);
    }
}
