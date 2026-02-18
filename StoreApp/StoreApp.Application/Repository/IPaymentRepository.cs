using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Repository
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<List<Payment>> SearchByKeyword(string keyword);
        Task<Payment> GetByOrderId(Guid orderId);
    }
}
