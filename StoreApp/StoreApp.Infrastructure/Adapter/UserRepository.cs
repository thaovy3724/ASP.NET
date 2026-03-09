using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class UserRepository(StoreDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        private readonly DbSet<User> _dbset = context.Set<User>();

        public async Task<PagedList<User>> Search(int pageNumber, int pageSize, string? keyword = null)
        {
            var query = _dbset.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(x =>
                    x.Username.Contains(keyword) ||
                    x.FullName.Contains(keyword) ||
                    x.Role.ToString().Contains(keyword));
            }
            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public Task<User?> GetByName(string name)
        {
            return _dbset.AsNoTracking().FirstOrDefaultAsync(x => x.Username == name);
        }

        public Task<User?> GetByEmail(string email)
        {
            return _dbset.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
