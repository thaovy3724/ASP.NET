using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Application.Service.Security;
using StoreApp.Core.Entities;



namespace StoreApp.Application.UseCases.AuthUseCase.Command.Login
{
    public class LoginHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IAuthService authService) : IRequestHandler<LoginCommand, ResultWithData<TokenResponseDTO>>
    {
        public async Task<ResultWithData<TokenResponseDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByUserName(request.userName);
            if (user is null)
            {
                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            if (passwordHasher.VerifyHashedPassword(user, user.Password, request.password)
                == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            return new ResultWithData<TokenResponseDTO>(
                Success: true,
                Message: "Thêm thể loại thành công",
                Data: await authService.CreateTokenResponse(user)
                );
        }
    }
}
