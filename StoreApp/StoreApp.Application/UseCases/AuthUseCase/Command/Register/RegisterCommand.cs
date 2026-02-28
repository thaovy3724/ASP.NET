using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Register
{
    public sealed record RegisterCommand(
        string UserName,
        string FullName,
        string Password,
        string Phone
    ) : IRequest<UserDTO>;

}
