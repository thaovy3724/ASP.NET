using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Query.GetOne
{
    public sealed record GetCustomerAddressQuery(Guid Id, Guid? CustomerId) : IRequest<CustomerAddressDTO>;
}
