using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne
{
    public sealed record GetCategoryQuery(Guid Id) : IRequest<ResultWithData<CategoryDTO>>;
}
