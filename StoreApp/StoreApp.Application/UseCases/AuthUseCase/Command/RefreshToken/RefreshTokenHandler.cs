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

            var now = DateTime.UtcNow;

            // Nếu khóa tạm thời đã hết hạn thì tự clear
            var hadExpiredLockout = user.LockoutEnd.HasValue && user.LockoutEnd.Value <= now;
            user.ClearExpiredLockout(now);

            if (hadExpiredLockout)
            {
                await userRepository.Update(user);
            }

            // Nếu tài khoản đang bị khóa tạm thời thì không cho refresh token
            if (user.IsTemporarilyLocked(now))
            {
                var remainMinutes = Math.Ceiling((user.LockoutEnd!.Value - now).TotalMinutes);
                throw new UnauthorizedAccessException($"Tài khoản đang bị khóa tạm thời. Vui lòng thử lại sau khoảng {remainMinutes} phút.");
            }

            return await authService.CreateTokenResponse(user);
        }
    }
}
