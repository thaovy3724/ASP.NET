using MediatR;
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
    public sealed record CreateUserHandler(IUserRepository UserRepository) : IRequestHandler<CreateUserCommand, ResultWithData<UserDTO>>
    {
        public async Task<ResultWithData<UserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if(await UserRepository.isUsernameExist(request.userName))
            {
                return new ResultWithData<UserDTO>(
                    Success: false,
                    Message: "Tên đăng nhập đã tồn tại.",
                    Data: null
                );
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
                    return new ResultWithData<UserDTO>(
                        Success: false,
                        Message: "Vai trò không hợp lệ.",
                        Data: null
                    );
            }
            var user = new User
            (
                request.userName,
                request.fullName,
                request.password,
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
