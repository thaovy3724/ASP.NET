using MediatR;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Update
{
    public sealed record UpdateUserCommand(
        Guid Id,
        string UserName,
        string FullName,
        string Phone,
        string Email,
        string Role,
        bool IsActive) : IRequest<Unit>;
}