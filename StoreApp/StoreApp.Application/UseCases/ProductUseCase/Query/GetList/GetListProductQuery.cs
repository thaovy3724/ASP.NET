using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetList
{
    public sealed record GetListProductQuery
        (Guid? CategoryId = null, 
        decimal? MinPrice = null, 
        decimal? MaxPrice = null, 
        string? Keyword = null) : QueryStringParameters, IRequest<List<ProductDTO>>;
}
