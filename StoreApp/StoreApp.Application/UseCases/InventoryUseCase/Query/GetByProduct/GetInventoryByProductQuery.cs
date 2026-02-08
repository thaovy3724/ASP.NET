using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Query.GetByProduct
{
    public sealed record GetInventoryByProductQuery(Guid ProductId) : IRequest<ResultWithData<InventoryDTO>>;
}
