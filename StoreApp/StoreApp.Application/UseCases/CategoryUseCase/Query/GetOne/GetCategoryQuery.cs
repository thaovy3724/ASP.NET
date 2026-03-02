using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.CategoryUseCase.Query.GetOne
{
    public sealed record GetCategoryQuery(Guid Id) : IRequest<CategoryDTO>;
}
