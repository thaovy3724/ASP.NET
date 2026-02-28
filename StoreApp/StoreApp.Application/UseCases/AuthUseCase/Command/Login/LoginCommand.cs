using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Login
{
    public sealed record LoginCommand(string userName, string password) : IRequest<TokenResponseDTO>;
}
