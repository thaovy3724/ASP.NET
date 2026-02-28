using MediatR;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Delete
{
    public sealed record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;
}
