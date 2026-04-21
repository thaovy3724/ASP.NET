using MediatR;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Delete
{
    public sealed record DeleteCustomerAddressCommand(Guid Id, Guid? CustomerId) : IRequest<Unit>;
}
