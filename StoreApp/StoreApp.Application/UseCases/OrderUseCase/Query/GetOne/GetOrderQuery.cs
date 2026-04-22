using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.GetOne
{
    public sealed record GetOrderQuery(
        Guid Id,
        Guid? CustomerId = null
    ) : IRequest<OrderDTO>;
}