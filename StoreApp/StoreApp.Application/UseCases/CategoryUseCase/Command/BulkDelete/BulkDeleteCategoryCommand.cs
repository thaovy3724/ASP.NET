using MediatR;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.BulkDelete
{
    public sealed record BulkDeleteCategoryCommand(List<Guid> Ids) : IRequest<Unit>;
}
