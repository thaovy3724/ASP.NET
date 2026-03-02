using MediatR;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Update
{
    public sealed record UpdateCategoryCommand(Guid Id, string Name) : IRequest<Unit>;
}
