using MediatR;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Update
{
    public sealed record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Result>;
}
