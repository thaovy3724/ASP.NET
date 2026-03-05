using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Create
{
    public class CreateUserHandler(IUserRepository UserRepository, IPasswordHasher<User> PasswordHasher) : IRequestHandler<CreateUserCommand, UserDTO>
    {
        public async Task<UserDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await UserRepository.IsExist(u => u.Username == request.UserName))
            {
                throw new ConflictException("Tên đăng nhập đã tồn tại.");
            }

            var role = request.Role switch
            {
                "Admin" => Role.Admin,
                "Staff" => Role.Staff,
                "Customer" => Role.Customer,
                _ => throw new ArgumentException("Vai trò người dùng không hợp lệ(Admin/Staff/Customer)"),
            };

            var hashedPassword = PasswordHasher.HashPassword(null, request.Password);
            var user = new User
            (
                request.UserName,
                hashedPassword,
                request.FullName,
                request.Phone,
                role,
                DateTime.Now
            );

            await UserRepository.Create(user);
            return user.ToDTO();
        }
    }
}
