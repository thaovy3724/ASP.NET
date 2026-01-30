using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Update
{
    public sealed record UpdateProductCommand(
        Guid ProductId,
        Guid CategoryId,
        Guid SupplierId,
        string ProductName,
        string Barcode,
        decimal Price,
        string Unit,
        string? ImageUrl 
    ) : IRequest<Result>;
}
