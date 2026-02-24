using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetList
{
    public sealed record GetListCategoryQuery(
        string? Keyword = null
    ) : IRequest<ResultWithData<List<CategoryDTO>>>;
}
