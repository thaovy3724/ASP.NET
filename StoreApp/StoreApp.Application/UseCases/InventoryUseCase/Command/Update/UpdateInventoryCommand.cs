using MediatR;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Command.Update
{
    public sealed record UpdateInventoryCommand(Guid InventoryId, int Quantity) : IRequest<Result>;
}
