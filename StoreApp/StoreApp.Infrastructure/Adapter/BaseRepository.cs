using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;
using System.Linq.Expressions;

namespace StoreApp.Infrastructure.Adapter
{
    public class BaseRepository<T>(StoreDbContext context) : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> DbSet = context.Set<T>();
        private IDbContextTransaction? _currentTransaction;
        public async Task BeginTransactionAsync()
        {
            // Kiểm tra nếu đã có transaction đang chạy thì không tạo mới
            if (_currentTransaction != null) return;

            _currentTransaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

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
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            DbSet.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task Delete(T entity)
        {
            DbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsNoTracking().AnyAsync(predicate);
        }
    }
}
