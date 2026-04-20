using MediatR;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Lock
{
    public sealed record LockUserCommand(Guid Id, Guid CurrentUserId) : IRequest<Unit>;
}