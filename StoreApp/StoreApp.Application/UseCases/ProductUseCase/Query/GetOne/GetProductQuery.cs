using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetOne
{
    public sealed record GetProductQuery(Guid Id) : IRequest<ProductDetailDTO>;
}