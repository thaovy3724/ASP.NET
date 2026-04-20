using MediatR;

namespace StoreApp.Application.UseCases.UserUseCase.Command.BulkDelete
{
    public sealed record BulkDeleteUserCommand(
        List<Guid>? Ids,
        Guid CurrentUserId = default
    ) : IRequest<Unit>;
}