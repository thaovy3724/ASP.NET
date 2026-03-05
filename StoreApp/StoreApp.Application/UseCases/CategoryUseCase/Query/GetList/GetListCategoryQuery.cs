using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetList
{
    public sealed record GetListCategoryQuery(string? Keyword = null) : QueryStringParameters, IRequest<PagedList<CategoryDTO>>;
}
