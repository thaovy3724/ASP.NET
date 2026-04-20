using MediatR;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Unlock
{
    public sealed record UnlockUserCommand(Guid Id) : IRequest<Unit>;
}