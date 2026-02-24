using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Delete
{
    public class DeleteUserHandler(IUserRepository userRepository, IOrderRepository orderRepository) : IRequestHandler<DeleteUserCommand, Result>
    {
        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetById(request.Id);

            if (user is null)
            {
                throw new NotFoundException("Không tìm thấy người dùng.");
            }

            if(await orderRepository.IsExist(o => o.CustomerId == request.Id || o.StaffId == request.Id))
            {
                throw new ConflictException("Có thông tin đơn hàng của người dùng. Không thể thực hiện xóa người dùng.");
            }   

            await userRepository.Delete(user);
            return new Result(
                Success: true,
                Message: "Xóa người dùng thành công."
            );
        }
    }
}
