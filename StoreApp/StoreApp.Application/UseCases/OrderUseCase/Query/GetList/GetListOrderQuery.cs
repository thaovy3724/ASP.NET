using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.GetList
{
    public sealed record GetListOrderQuery : IRequest<List<OrderDTO>>;
}
