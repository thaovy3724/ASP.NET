using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.Search
{
    public sealed record SearchCategoryQuery(string Keyword) : IRequest<ResultWithData<List<CategoryDTO>>>;
}
