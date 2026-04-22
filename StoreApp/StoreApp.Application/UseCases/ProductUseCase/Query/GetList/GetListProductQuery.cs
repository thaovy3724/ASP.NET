using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetList
{
    public sealed record GetListProductQuery(
        Guid? CategoryId = null,
        Guid? SupplierId = null,
        decimal? MinPrice = null,
        decimal? MaxPrice = null,
        int? MinQuantity = null,
        int? MaxQuantity = null,
        string? Keyword = null,
        string? SortBy = null,
        bool IsDescending = true
    ) : QueryStringParameters, IRequest<PagedList<ProductDTO>>;
}