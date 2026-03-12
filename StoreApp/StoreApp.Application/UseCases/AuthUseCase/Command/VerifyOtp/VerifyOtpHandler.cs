using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.Service.Email;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.VerifyOtp
{
    public class VerifyOtpHandler(IUserRepository UserRepository, IOtpService OtpService) : IRequestHandler<VerifyOtpCommand, VerifyOtpResponseDTO>
    {
        public async Task<VerifyOtpResponseDTO> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {

            // 1. Kiểm tra mã OTP
            var user = OtpService.ValidateOtp(request.Email, request.Otp);
            if (user is null)
            {
                throw new ConflictException("Mã OTP không đúng hoặc đã hết hạn.");
            }

            // 2. Logic kích hoạt tài khoản trong Database
            await UserRepository.Create(user);

            // 3. XÓA OTP NGAY TẠI ĐÂY
            OtpService.ClearOtp(request.Email);

            return new VerifyOtpResponseDTO(user.Username, user.FullName);
        }
    }
}
