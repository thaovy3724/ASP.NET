using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class UserRepository(StoreDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        private readonly DbSet<User> _dbset = context.Set<User>();
        public List<User> SearchByKeyword(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return _dbset.ToList();
            return _dbset.Where(x =>
                x.Username.Contains(keyword) ||
                x.FullName.Contains(keyword) ||
                x.Role.ToString().Contains(keyword)).ToList();
        }
        public async Task<bool> isUsernameExist(string username)
        {
            return await _dbset.AnyAsync(x => x.Username == username);
        }

        public async Task<bool> isUserExist(Guid userId)
        {
            return await _dbset.AnyAsync(x => x.Id == userId);
        }
    }
}
