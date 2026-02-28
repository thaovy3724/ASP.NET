using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Update
{
    public class UpdateUserHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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
            return Unit.Value;
        }
    }
}
