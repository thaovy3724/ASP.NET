using MediatR;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.BulkDelete
{
    public sealed record BulkDeleteProductCommand(List<Guid>? Ids) : IRequest<Unit>;
}