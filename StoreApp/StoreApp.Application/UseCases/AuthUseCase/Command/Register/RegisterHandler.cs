using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Ports;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Register
{
    public class RegisterHandler(IUserRepository UserRepository, IPasswordHasher<User> PasswordHasher, IOtpService OtpService) : IRequestHandler<RegisterCommand, UserDTO>
    {
        public async Task<UserDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra Username
            if (await UserRepository.IsExist(u => u.Username == request.UserName))
            {
                throw new ConflictException("Tên đăng nhập đã tồn tại.");
            }

            // Hash mật khẩu
            var hashedPassword = PasswordHasher.HashPassword(null!, request.Password);

            // Khởi tạo thực thể User
            var user = new User
            (
                request.UserName,
                hashedPassword,
                request.FullName,
                request.Phone,
                Role.Customer,
                DateTime.Now
            );

            user.IsActive = false;

            // Nếu gửi Mail lỗi, người dùng có thể dùng chức năng "Resend OTP"
            await OtpService.SendAndCacheOtpAsync(request.UserName, request.FullName);

            await UserRepository.Create(user);
            return user.ToDTO();
        }
    }
}
