using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Command.SetDefault
{
    public class SetDefaultCustomerAddressHandler(
        ICustomerAddressRepository addressRepository
    ) : IRequestHandler<SetDefaultCustomerAddressCommand, CustomerAddressDTO>
    {
        public async Task<CustomerAddressDTO> Handle(SetDefaultCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerId = request.CustomerId!.Value;
            var address = await addressRepository.GetByIdAndCustomerIdAsync(request.Id, customerId);

            if (address == null)
                throw new NotFoundException("Không tìm thấy địa chỉ giao hàng của bạn.");

            await addressRepository.ResetDefaultAsync(customerId);
            address.SetDefault();
            await addressRepository.Update(address);

            return address.ToDTO();
        }
    }
}
