using MediatR;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Delete
{
    public sealed record DeleteCategoryCommand(Guid Id) : IRequest<Result>;
}
