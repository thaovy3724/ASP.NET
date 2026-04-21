using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Query.GetList
{
    public sealed record GetListCustomerAddressQuery(Guid? CustomerId) : IRequest<List<CustomerAddressDTO>>;
}
