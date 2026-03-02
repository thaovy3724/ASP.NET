using MediatR;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Delete
{
    public sealed record DeleteUserCommand(Guid Id) : IRequest<Unit>;
}
