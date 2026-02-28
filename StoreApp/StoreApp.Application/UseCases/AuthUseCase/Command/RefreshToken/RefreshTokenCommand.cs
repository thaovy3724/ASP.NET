using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.RefreshToken
{
    public sealed record RefreshTokenCommand(Guid userId, string refreshToken) : IRequest<TokenResponseDTO>;
}
