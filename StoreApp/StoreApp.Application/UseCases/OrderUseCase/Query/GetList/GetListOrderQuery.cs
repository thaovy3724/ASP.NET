using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.GetList
{
    public sealed record GetListOrderQuery(Guid? CustomerId = null) : QueryStringParameters, IRequest<PagedList<OrderDTO>>;
}
