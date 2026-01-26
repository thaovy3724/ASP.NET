using StoreApp.Core.Entities;

namespace StoreApp.Application.Ports.Output
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        // search by keyword
        Task<List<Customer>> SearchByKeyword(string? keyword);


    }
}
