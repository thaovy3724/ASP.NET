using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.ProductReviewUseCase.Command.Create
{
    // Command to create a new product review
    public sealed record CreateProductReviewCommand(
        Guid ProductId,
        Guid? CustomerId,
        int Rating,
        string? Comment
    ) : IRequest<ProductReviewDTO>;

    // Request model for creating a product review
    public sealed record CreateProductReviewRequest(
        int Rating,
        string? Comment
    );
}   