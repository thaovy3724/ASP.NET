using MediatR;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Update
{
    public sealed record UpdateProductCommand(
        Guid Id,
        Guid CategoryId,
        Guid SupplierId,
        string ProductName,
        decimal Price,
        string? ImageUrl
    ) : IRequest<Unit>;
}
