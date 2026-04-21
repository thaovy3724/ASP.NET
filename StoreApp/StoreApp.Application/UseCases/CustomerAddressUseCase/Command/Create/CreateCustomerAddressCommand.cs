using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Create
{
    public sealed record CreateCustomerAddressCommand(
        Guid? CustomerId,
        string ReceiverName,
        string Phone,
        string AddressLine,
        bool IsDefault
    ) : IRequest<CustomerAddressDTO>;
}
