using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Service.Security;
using System;
namespace StoreApp.Application.UseCases.AuthUseCase.Command.Logout
{
    public class LogoutHandler(IAuthService authService, IUserRepository userRepository) : IRequestHandler<LogoutCommand, Unit>
    {
        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await authService.ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
            {
                throw new UnauthorizedAccessException("Refresh token không hợp lệ hoặc đã hết hạn");
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await userRepository.Update(user);
            return Unit.Value;
        }
    }
}
