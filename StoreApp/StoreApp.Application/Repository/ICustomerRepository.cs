using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        // search by keyword
        Task<List<Customer>> SearchByKeyword(string? keyword);
        Task<bool> IsEmailExists(string email);
        Task<bool> IsPhoneExists(string phone);


    }
}
