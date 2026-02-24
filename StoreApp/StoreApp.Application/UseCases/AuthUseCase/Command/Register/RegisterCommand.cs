using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Register
{
    public sealed record RegisterCommand(
        string UserName,
        string FullName,
        string Password,
        string Phone
    ) : IRequest<ResultWithData<UserDTO>>;

}
