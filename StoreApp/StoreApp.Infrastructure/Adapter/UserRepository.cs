using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public class UserRepository(DbContext context) : BaseRepository<User>(context), IUserRepository
    {
    }
}
