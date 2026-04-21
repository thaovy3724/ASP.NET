using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Delete
{
    public class DeleteCustomerAddressHandler(
        ICustomerAddressRepository addressRepository
    ) : IRequestHandler<DeleteCustomerAddressCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerId = request.CustomerId!.Value;
            var address = await addressRepository.GetByIdAndCustomerIdAsync(request.Id, customerId);

            if (address == null)
                throw new NotFoundException("Không tìm thấy địa chỉ giao hàng của bạn.");

            await addressRepository.Delete(address);
            return Unit.Value;
        }
    }
}
