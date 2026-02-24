using StoreApp.Core.Entities;
using System.Linq.Expressions;
namespace StoreApp.Application.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(Guid id);

        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity); // Xoá khỏi DB luôn 

        // Kiểm tra tồn tại theo điều kiện nào đó (ví dụ: tên đã tồn tại chưa)
        Task<bool> IsExist(Expression<Func<T, bool>> predicate); 
    }
}
