using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;
using StoreApp.Application.Service.Security;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Login
{
    public class LoginHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IAuthService authService) : IRequestHandler<LoginCommand, TokenResponseDTO>
    {
        public async Task<TokenResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Thiết lập giới hạn số lần đăng nhập thất bại và thời gian khóa tài khoản
            const int maxFailedAttempts = 5;
            var lockoutDuration = TimeSpan.FromMinutes(15);
            var now = DateTime.UtcNow;

            var user = await userRepository.GetByName(request.UserName);

            // Không tiết lộ rõ tài khoản có tồn tại hay không
            if (user is null)
            {
                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            // Nếu đã hết thời gian khóa thì reset trạng thái khóa
            user.ClearExpiredLockout(now);

            // Nếu vẫn đang trong thời gian khóa thì chặn đăng nhập
            if (user.IsTemporarilyLocked(now))
            {
                var remainMinutes = Math.Ceiling((user.LockoutEnd!.Value - now).TotalMinutes);
                throw new UnauthorizedAccessException($"Tài khoản đang bị khóa tạm thời. Vui lòng thử lại sau khoảng {remainMinutes} phút.");
            }

            var verifyResult = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (verifyResult == PasswordVerificationResult.Failed)
            {
                user.RegisterFailedLogin(now, maxFailedAttempts, lockoutDuration);
                await userRepository.Update(user);

                if (user.IsTemporarilyLocked(now))
                {
                    throw new UnauthorizedAccessException("Bạn đã nhập sai mật khẩu quá 5 lần. Tài khoản bị khóa tạm thời trong 15 phút.");
                }

                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            // Đăng nhập đúng thì reset số lần sai
            user.ResetLoginFailure();

            return await authService.CreateTokenResponse(user);
        }
    }
}
