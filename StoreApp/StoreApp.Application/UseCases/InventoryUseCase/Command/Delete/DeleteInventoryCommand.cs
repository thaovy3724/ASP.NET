using MediatR;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Command.Delete
{
    public sealed record DeleteInventoryCommand(Guid Id) : IRequest<Result>;
}
