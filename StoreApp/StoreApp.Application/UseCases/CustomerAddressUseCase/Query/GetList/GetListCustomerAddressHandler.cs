using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Query.GetList
{
    public class GetListCustomerAddressHandler(
        ICustomerAddressRepository addressRepository
    ) : IRequestHandler<GetListCustomerAddressQuery, List<CustomerAddressDTO>>
    {
        public async Task<List<CustomerAddressDTO>> Handle(GetListCustomerAddressQuery request, CancellationToken cancellationToken)
        {
            var customerId = request.CustomerId!.Value;
            var addresses = await addressRepository.GetByCustomerIdAsync(customerId);
            return addresses.Select(x => x.ToDTO()).ToList();
        }
    }
}
