using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetList
{
    public sealed record GetListCategoryQuery(string? Keyword = null) : IRequest<List<CategoryDTO>>;
}
