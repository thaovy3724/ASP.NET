using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetList
{
    public sealed record GetListProductQuery
        (Guid? CategoryId = null, 
        Guid? SupplierId = null,
        String? orderDate = null,
        decimal? MinPrice = null, 
        decimal? MaxPrice = null, 
        string? Keyword = null) : QueryStringParameters, IRequest<PagedList<ProductDTO>>;
}
