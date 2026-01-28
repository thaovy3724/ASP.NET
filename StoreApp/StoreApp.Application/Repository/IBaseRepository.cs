using StoreApp.Core.Entities;
using System;
namespace StoreApp.Application.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(Guid id);

        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity); // Xoá khỏi DB luôn 
    }
}
