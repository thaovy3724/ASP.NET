using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.Search
{
    public sealed record SearchProductQuery(Guid? SupplierId, Guid? CategoryId, decimal? MinPrice, decimal? MaxPrice, string? Keyword, string? PriceOrder) : IRequest<ResultWithData<List<ProductDTO>>>;
}
