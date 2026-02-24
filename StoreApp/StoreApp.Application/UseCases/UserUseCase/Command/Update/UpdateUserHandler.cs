using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
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
            var user = await userRepository.GetById(request.Id);
            if (user is null)
            {
                throw new NotFoundException("Không tìm thấy người dùng.");
            }

            var role = request.Role switch
            {
                "Admin" => Role.Admin,
                "Staff" => Role.Staff,
                "Customer" => Role.Customer,
                _ => throw new ArgumentException("Vai trò không hợp lệ(Admin/Staff/Customer)."),
            };
            user.Update(request.UserName, request.Password, request.FullName, request.Phone, role);
            await userRepository.Update(user);
            return new Result(
                Success: true,
                Message: "Cập nhật người dùng thành công."
            );
        }
    }
}
