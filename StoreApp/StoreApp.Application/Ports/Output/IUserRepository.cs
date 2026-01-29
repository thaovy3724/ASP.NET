using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Ports.Output
{
    public interface IUserRepository : IBaseRepository<User>
    {
        List<User> SearchByKeyword(string keyword);
        Task<bool> isUsernameExist(string username);
        Task<bool> isUserExist(Guid userId);
    }
}
