using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class UserRepository(StoreDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        private readonly DbSet<User> _dbset = context.Set<User>();

        public async Task<PagedList<User>> Search(int pageNumber, int pageSize, string? keyword = null, Role? role = null)
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
            if(role is not null)
            {
                query = query.Where(x => x.Role == role.Value);
            }
            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public Task<User?> GetByName(string name)
        {
            return _dbset.AsNoTracking().FirstOrDefaultAsync(x => x.Username == name);
        }
    }
}
