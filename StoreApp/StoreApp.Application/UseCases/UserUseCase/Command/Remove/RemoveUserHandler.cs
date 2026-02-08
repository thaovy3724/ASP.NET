using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Remove
{
    public class RemoveUserHandler(IUserRepository userRepository) : IRequestHandler<RemoveUserCommand, Result>
    {
        public async Task<Result> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            if (! await userRepository.isUserExist(request.Id))
            {
                throw new NotFoundException("Không tìm thấy người dùng.");
            }
            if(await userRepository.isExistUserOfOrder(request.Id))
            {
                throw new ConflictException("Có thông tin đơn hàng  của người dùng. Không thể thực hiện xóa người dùng.");
            }   
            var user = await userRepository.GetById(request.Id);
            await userRepository.Delete(user);
            return new Result(
                Success: true,
                Message: "Xóa người dùng thành công."
            );
        }
    }
}
