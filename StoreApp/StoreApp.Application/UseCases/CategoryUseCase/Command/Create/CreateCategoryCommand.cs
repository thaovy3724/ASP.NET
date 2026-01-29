using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Create
{
    public sealed record CreateCategoryCommand(string Name)
        : IRequest<ResultWithData<CategoryDTO>>;

    // sealed: ko cho phép kế thừa từ record này, tối ưu hiệu năng
    // record: immutable 
}
