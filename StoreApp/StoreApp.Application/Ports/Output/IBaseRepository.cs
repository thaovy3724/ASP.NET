using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Ports.Output
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(Guid id);

        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity); // Xoá khỏi DB luôn 
    }
}
