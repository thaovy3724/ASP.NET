using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetList
{
    public sealed record GetListProductQuery
        (Guid? CategoryId = null, 
        decimal? MinPrice = null, 
        decimal? MaxPrice = null, 
        string? Keyword = null) : IRequest<ResultWithData<List<ProductDTO>>>;
}
