using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.Service.Security;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Login
{
    public class LoginHandler(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IAuthService authService
    ) : IRequestHandler<LoginCommand, TokenResponseDTO>
    {
        private const int MaxFailedLoginCount = 5;
        private static readonly TimeSpan LockoutDuration = TimeSpan.FromMinutes(5);

        public async Task<TokenResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByName(request.UserName);

            if (user is null)
            {
                // Không nên nói quá rõ tài khoản tồn tại hay không
                throw new UnauthorizedAccessException("Tài khoản hoặc mật khẩu không chính xác.");
            }

            var now = DateTime.Now;

            // Nếu đang trong thời gian khóa thì chặn đăng nhập
            if (user.IsLoginLocked(now))
            {
                var remainingSeconds = (int)Math.Ceiling((user.LockoutEnd!.Value - now).TotalSeconds);

                throw new AccountLockedException(
                    $"Tài khoản đang tạm khóa do đăng nhập sai quá nhiều lần. Vui lòng thử lại sau {remainingSeconds} giây."
                );
            }

            // Nếu đã hết thời gian khóa thì reset để người dùng được thử lại từ đầu
            if (user.IsLockoutExpired(now))
            {
                user.ResetFailedLogin();
            }

            var verifyResult = passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                request.Password
            );

            // Sai mật khẩu
            if (verifyResult == PasswordVerificationResult.Failed)
            {
                user.IncreaseFailedLogin(
                    MaxFailedLoginCount,
                    LockoutDuration,
                    now
                );

                await userRepository.Update(user);

                if (user.IsLoginLocked(DateTime.Now))
                {
                    throw new AccountLockedException(
                        "Bạn đã nhập sai mật khẩu 5 lần. Tài khoản bị khóa đăng nhập trong 5 phút."
                    );
                }

                var remainingTry = MaxFailedLoginCount - user.FailedLoginCount;

                throw new UnauthorizedAccessException(
                    $"Tài khoản hoặc mật khẩu không chính xác. Bạn còn {remainingTry} lần thử."
                );
            }

            // Đăng nhập đúng thì reset số lần sai
            user.ResetFailedLogin();

            return await authService.CreateTokenResponse(user);
        }
    }
}