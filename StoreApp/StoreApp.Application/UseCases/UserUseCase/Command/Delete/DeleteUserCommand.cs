using MediatR;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Delete
{
    public sealed record DeleteUserCommand(Guid Id) : IRequest<Result>;
}
