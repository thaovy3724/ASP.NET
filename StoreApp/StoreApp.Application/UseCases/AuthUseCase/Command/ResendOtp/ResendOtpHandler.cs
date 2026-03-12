using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.Service.Email;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.ResendOtp
{
    public class ResendOtpHandler(IUserRepository userRepository, IOtpService OtpService) : IRequestHandler<ResendOtpCommand, bool>
    {
        public async Task<bool> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByName(request.UserName);
            if (user is not null)
            {
                throw new ConflictException("Tài khoản đã được xác thực trước đó");
            }

            // Gọi lại phương thức gửi OTP
            await OtpService.ResendAndCachedOtpAsync(request.UserName);

            return true;
        }
    }
}
