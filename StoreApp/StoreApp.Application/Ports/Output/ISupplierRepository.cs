using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Ports.Output
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        List<Supplier> SearchByKeyword(string keyword);
        Task<bool> IsSupplierIdExist(Guid supplierId);
        Task<bool> IsSupplierExist(string name, string email, string phone, Guid? ignoreId = null);
    }
}
