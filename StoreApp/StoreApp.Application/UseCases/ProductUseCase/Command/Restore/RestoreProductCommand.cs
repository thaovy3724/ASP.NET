using MediatR;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Restore
{
    public sealed record RestoreProductCommand(Guid Id) : IRequest<Unit>;
}