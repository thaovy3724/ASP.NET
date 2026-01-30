using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Query.GetOne
{
    public sealed record GetInventoryQuery(Guid Id) : IRequest<ResultWithData<InventoryDTO>>;
}
