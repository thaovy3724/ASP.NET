using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Repository
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {

        List<Supplier> SearchByKeyword(string keyword);
        Task<bool> IsSupplierIdExist(Guid supplierId);
        Task<bool> IsSupplierNameExist(string name, Guid? ignoreId = null);
        Task<bool> IsSupplierEmailExist(string email, Guid? ignoreId = null);
        Task<bool> IsSupplierPhoneExist(string phone, Guid? ignoreId = null);
        Task<bool> IsSupplierAddressExist(string address, Guid? ignoreId = null);
        Task<bool> IsSupplierExist(string name, string email, string phone, Guid? ignoreId = null);
    }
}
