using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Update
{
    public class UpdateUserHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, Result>
    {
        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (! await userRepository.isUserExist(request.Id))
            {
                throw new NotFoundException("Không tìm thấy người dùng.");
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
                    throw new ArgumentException("Vai trò không hợp lệ(Admin/Staff).");
            }
            var user = await userRepository.GetById(request.Id);
            user.Update(request.userName, request.password, request.fullName, role);
            await userRepository.Update(user);
            return new Result(
                Success: true,
                Message: "Cập nhật người dùng thành công."
            );
        }
    }
}
