using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Create
{
    public sealed record CreateUserCommand(
        string UserName, // Email as username
        string FullName, 
        string Password, 
        string Phone,
        string Role) : IRequest<UserDTO>;
}
