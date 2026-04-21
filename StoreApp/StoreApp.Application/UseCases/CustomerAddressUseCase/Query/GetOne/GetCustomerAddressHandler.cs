using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Query.GetOne
{
    public class GetCustomerAddressHandler(
        ICustomerAddressRepository addressRepository
    ) : IRequestHandler<GetCustomerAddressQuery, CustomerAddressDTO>
    {
        public async Task<CustomerAddressDTO> Handle(GetCustomerAddressQuery request, CancellationToken cancellationToken)
        {
            var address = await addressRepository.GetByIdAndCustomerIdAsync(request.Id, request.CustomerId!.Value);

            if (address == null)
                throw new NotFoundException("Không tìm thấy địa chỉ giao hàng của bạn.");

            return address.ToDTO();
        }
    }
}
