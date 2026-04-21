using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.ProductReviewUseCase.Query.GetByProduct
{
    public sealed record GetProductReviewsQuery(Guid ProductId) : IRequest<List<ProductReviewDTO>>;
}