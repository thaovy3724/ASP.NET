using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class UserRepository(StoreDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        private readonly DbSet<User> _dbset = context.Set<User>();
        public async Task<List<User>> SearchByKeyword(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return await _dbset.ToListAsync();
            return await _dbset.Where(x =>
                x.Username.Contains(keyword) ||
                x.FullName.Contains(keyword) ||
                x.Role.ToString().Contains(keyword)).ToListAsync();
        }
        public async Task<bool> isUsernameExist(string username)
        {
            return await _dbset.AnyAsync(x => x.Username == username);
        }

        public async Task<bool> isUserExist(Guid userId)
        {
            return await _dbset.AnyAsync(x => x.Id == userId);
        }
        public async Task<bool> isExistUserOfOrder(Guid userId)
        {
            return await context.Set<Order>()
                          .AsNoTracking()
                          .AnyAsync(o => o.UserId == userId);
        }
    }
}
