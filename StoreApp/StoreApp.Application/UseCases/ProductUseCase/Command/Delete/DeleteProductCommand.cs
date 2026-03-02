using MediatR;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Delete
{
    public sealed record DeleteProductCommand(Guid Id) : IRequest<Unit>;
}
