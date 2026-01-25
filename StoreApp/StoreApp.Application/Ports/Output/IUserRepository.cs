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
    }
}
