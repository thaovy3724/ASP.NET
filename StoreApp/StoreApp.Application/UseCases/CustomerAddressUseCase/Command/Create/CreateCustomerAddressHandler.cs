using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Create
{
    public class CreateCustomerAddressHandler(
        ICustomerAddressRepository addressRepository,
        IUserRepository userRepository
    ) : IRequestHandler<CreateCustomerAddressCommand, CustomerAddressDTO>
    {
        public async Task<CustomerAddressDTO> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerId = request.CustomerId!.Value;

            var customerExists = await userRepository.IsExist(x => x.Id == customerId && x.Role == Role.Customer);
            if (!customerExists)
                throw new NotFoundException("Khách hàng không tồn tại.");

            var hasAddress = await addressRepository.HasAnyByCustomerIdAsync(customerId);
            var shouldBeDefault = request.IsDefault || !hasAddress;

            if (shouldBeDefault)
                await addressRepository.ResetDefaultAsync(customerId);

            var address = new CustomerAddress(
                customerId,
                request.ReceiverName.Trim(),
                request.Phone.Trim(),
                request.AddressLine.Trim(),
                shouldBeDefault
            );

            await addressRepository.Create(address);
            return address.ToDTO();
        }
    }
}
