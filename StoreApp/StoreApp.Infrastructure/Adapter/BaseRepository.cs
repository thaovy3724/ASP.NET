using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Adapter
{
    public class BaseRepository<T>(StoreDbContext Context) : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> DbSet = Context.Set<T>();

        public async Task<List<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }
        public async Task Delete(T entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsNoTracking().AnyAsync(predicate);
        }
    }
}
