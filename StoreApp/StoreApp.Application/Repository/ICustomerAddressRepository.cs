using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface ICustomerAddressRepository : IBaseRepository<CustomerAddress>
    {
        Task<List<CustomerAddress>> GetByCustomerIdAsync(Guid customerId);
        Task<CustomerAddress?> GetByIdAndCustomerIdAsync(Guid id, Guid customerId);
        Task<CustomerAddress?> GetDefaultByCustomerIdAsync(Guid customerId);
        Task<bool> HasAnyByCustomerIdAsync(Guid customerId);
        Task ResetDefaultAsync(Guid customerId);
    }
}
