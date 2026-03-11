using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Ports;
using StoreApp.Application.Repository;
using StoreApp.Application.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.ResendOtp
{
    public class ResendOtpHandler(IUserRepository userRepository, IOtpService OtpService) : IRequestHandler<ResendOtpCommand, bool>
    {
        public async Task<bool> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra xem có đang bị khóa gửi lại OTP không
            if (OtpService.IsResendLocked(request.Email))
            {
                throw new BadRequestException("Vui lòng đợi 60 giây trước khi yêu cầu mã mới.");
            }

            var user = await userRepository.GetByName(request.Email);
            if (user == null)
            {
                return false; 
            }

            if (user.IsActive)
            {
                throw new BadRequestException("Tài khoản này đã được xác thực trước đó.");
            }
            // Gọi lại phương thức gửi OTP
            await OtpService.SendAndCacheOtpAsync(request.Email, user.FullName);

            return true;
        }
    }
}
