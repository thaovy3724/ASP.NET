using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Update
{
    public class UpdateCustomerAddressHandler(
        ICustomerAddressRepository addressRepository
    ) : IRequestHandler<UpdateCustomerAddressCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerId = request.CustomerId!.Value;
            var address = await addressRepository.GetByIdAndCustomerIdAsync(request.Id, customerId);

            if (address == null)
                throw new NotFoundException("Không tìm thấy địa chỉ giao hàng của bạn.");

            address.Update(
                request.ReceiverName.Trim(),
                request.Phone.Trim(),
                request.AddressLine.Trim()
            );

            if (request.IsDefault)
            {
                await addressRepository.ResetDefaultAsync(customerId);
                address.SetDefault();
            }

            await addressRepository.Update(address);
            return Unit.Value;
        }
    }
}
