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
            if(user.failedLoginCount >= 5)
            {
                user.lockoutEnd = 1;
                user.lockedDate = DateTime.Now;
                await userRepository.Update(user);
            }
            if(user.lockoutEnd == 1)
            {
                throw new UnauthorizedAccessException("Tài khoản đang bị khóa, vui lòng ko thực hiện đăng nhập trong 15 phút nữa!");
            }
            
            if (passwordHasher.VerifyHashedPassword(user, user.Password, request.Password)
                == PasswordVerificationResult.Failed)
            {
                user.failedLoginCount++;
                await userRepository.Update(user);
                throw new UnauthorizedAccessException("Mật khẩu chưa chính xác");
            }
            user.lockoutEnd = 0;
            user.failedLoginCount = 0;
            await userRepository.Update(user);
            return await authService.CreateTokenResponse(user);
        }
    }
}
