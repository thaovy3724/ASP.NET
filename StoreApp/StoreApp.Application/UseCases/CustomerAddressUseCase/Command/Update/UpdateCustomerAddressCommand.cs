using MediatR;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Update
{
    public sealed record UpdateCustomerAddressCommand(
        Guid Id,
        Guid? CustomerId,
        string ReceiverName,
        string Phone,
        string AddressLine,
        bool IsDefault
    ) : IRequest<Unit>;
}
