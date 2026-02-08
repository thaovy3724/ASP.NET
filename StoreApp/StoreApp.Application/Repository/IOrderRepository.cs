using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Repository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> Search(string? keyword);
    }
}
