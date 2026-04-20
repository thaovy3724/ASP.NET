using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;
using StoreApp.Application.Service.Security;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.RefreshToken
{
    public class RefreshTokenHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IAuthService authService) : IRequestHandler<RefreshTokenCommand, TokenResponseDTO>
    {
        public async Task<TokenResponseDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await authService.ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
            {
                throw new UnauthorizedAccessException("Refresh token không hợp lệ hoặc đã hết hạn");
            }

            if (user.IsLocked)
            {
                throw new UnauthorizedAccessException("Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên.");
            }
            return await authService.CreateTokenResponse(user);
        }
    }
}
