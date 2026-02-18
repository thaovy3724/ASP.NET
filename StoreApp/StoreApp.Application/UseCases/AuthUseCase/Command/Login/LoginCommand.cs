using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Login
{
    public sealed record LoginCommand(string userName, string password) : IRequest<ResultWithData<TokenResponseDTO>>;
}
