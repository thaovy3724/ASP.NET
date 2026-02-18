using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Create
{
    public sealed record CreateUserHandler(IUserRepository UserRepository, IPasswordHasher<User> PasswordHasher) : IRequestHandler<CreateUserCommand, ResultWithData<UserDTO>>
    {
        public async Task<ResultWithData<UserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await UserRepository.IsUsernameExist(request.userName))
            {
                throw new ConflictException("Tên đăng nhập đã tồn tại.");
            }
            Role role;
            switch (request.role)
            {
                case "Admin":
                    role = Role.Admin;
                    break;
                case "Staff":
                    role = Role.Staff;
                    break;
                default:
                    throw new ArgumentException("Vai trò người dùng không hợp lệ(Admin/Staff)");
            }

            var hashedPassword = PasswordHasher.HashPassword(null, request.password); // Fixed the issue here
            var user = new User
            (
                request.userName,
                request.fullName,
                hashedPassword, // Use the hashed password here
                role,
                DateTime.Now
            );
            await UserRepository.Create(user);
            return new ResultWithData<UserDTO>(
                Success: true,
                Message: "Tạo người dùng thành công.",
                Data: user.ToDTO()
            );
        }
    }
}
