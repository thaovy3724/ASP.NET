using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Ports;
using StoreApp.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.VerifyOtp
{
    public class VerifyOtpHandler(IUserRepository UserRepository, IOtpService OtpService) : IRequestHandler<VerifyOtpCommand, bool>
    {
        public async Task<bool> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {

            // 1. Kiểm tra mã OTP
            if (!OtpService.ValidateOtp(request.Email, request.Otp))
            {
                throw new BadRequestException("Mã OTP không đúng hoặc đã hết hạn.");
            }

            // 2. Logic kích hoạt tài khoản trong Database
            var user = await UserRepository.GetByEmail(request.Email);
            user.Activate();
            await UserRepository.Update(user);

            // 3. XÓA OTP NGAY TẠI ĐÂY
            OtpService.ClearOtp(request.Email);

            return true;
        }
    }
}
