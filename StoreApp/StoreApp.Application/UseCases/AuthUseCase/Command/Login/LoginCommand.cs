using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Login
{
    public sealed record LoginCommand(string UserName, string Password) : IRequest<TokenResponseDTO>;
}
