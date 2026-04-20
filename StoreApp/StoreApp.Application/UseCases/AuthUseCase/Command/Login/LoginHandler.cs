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
            var user = await userRepository.GetByName(request.UserName);
            if (user is null)
            {
                throw new UnauthorizedAccessException("Tài khoản không tồn tại.");
            }

            if (user.IsLocked)
            {
                throw new UnauthorizedAccessException("Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên.");
            }

            if (passwordHasher.VerifyHashedPassword(user, user.Password, request.Password)
                == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Mật khẩu chưa chính xác");
            }

            return await authService.CreateTokenResponse(user);
        }
    }
}
