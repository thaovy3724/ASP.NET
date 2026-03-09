using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Update
{
    public class UpdateUserHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher) : IRequestHandler<UpdateUserCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetById(request.Id);
            if (user is null)
            {
                throw new NotFoundException("Không tìm thấy người dùng.");
            }

            // Không cho trùng Username với user khác
            if (await userRepository.IsExist(u => u.Username == request.UserName && u.Id != request.Id))
            {
                throw new ConflictException("Tên đăng nhập đã tồn tại.");
            }

            var role = request.Role switch
            {
                "Admin" => Role.Admin,
                "Staff" => Role.Staff,
                "Customer" => Role.Customer,
                _ => throw new ArgumentException("Vai trò không hợp lệ(Admin/Staff/Customer)."),
            };

            // Nếu Password rỗng -> giữ nguyên password hiện tại.
            var hashedPassword = user.Password;
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                hashedPassword = passwordHasher.HashPassword(user, request.Password!);
            }

            user.Update(request.UserName, hashedPassword, request.FullName, request.Phone, request.Email, role, request.IsActive);
            await userRepository.Update(user);

            return Unit.Value;
        }
    }
}