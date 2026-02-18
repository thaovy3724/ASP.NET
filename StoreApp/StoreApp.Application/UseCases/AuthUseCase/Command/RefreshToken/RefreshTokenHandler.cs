using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Application.Service.Security;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.RefreshToken
{
    public class RefreshTokenHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IAuthService authService) : IRequestHandler<RefreshTokenCommand, ResultWithData<TokenResponseDTO>>
    {
        public async Task<ResultWithData<TokenResponseDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await authService.ValidateRefreshTokenAsync(request.userId, request.refreshToken);
            if (user is null)
                return null;

            return new ResultWithData<TokenResponseDTO>(
                Success: true,
                Message: "Thêm thể loại thành công",
                Data: await authService.CreateTokenResponse(user)
                );
        }
    }
}
