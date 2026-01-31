using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Command.Create
{
    public sealed record CreateInventoryCommand(Guid ProductId, int Quantity) : IRequest<ResultWithData<InventoryDTO>>;
}
