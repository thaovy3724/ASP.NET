using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Command.SetDefault
{
    public sealed record SetDefaultCustomerAddressCommand(Guid Id, Guid? CustomerId) : IRequest<CustomerAddressDTO>;
}
