using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Register
{
    public class RegisterHandler(IUserRepository UserRepository, IPasswordHasher<User> PasswordHasher) : IRequestHandler<RegisterCommand, UserDTO>
    {
        public async Task<UserDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await UserRepository.IsExist(u => u.Username == request.UserName))
            {
                throw new ConflictException("Tên đăng nhập đã tồn tại.");
            }

            var role = Role.Customer; // Chỉ cho phép đăng ký với vai trò Customer

            var hashedPassword = PasswordHasher.HashPassword(null, request.Password);
            var user = new User
            (
                request.UserName,
                request.FullName,
                hashedPassword,
                request.Phone,
                role,
                DateTime.Now
            );

            await UserRepository.Create(user);
            return user.ToDTO();
        }
    }
}
