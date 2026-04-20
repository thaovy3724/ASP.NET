using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Lock
{
    public class LockUserHandler(IUserRepository userRepository) : IRequestHandler<LockUserCommand, Unit>
    {
        public async Task<Unit> Handle(LockUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == request.CurrentUserId)
            {
                throw new BadRequestException("Bạn không thể tự khóa tài khoản đang đăng nhập.");
            }

            var user = await userRepository.GetById(request.Id);
            if (user is null)
            {
                throw new NotFoundException("Không tìm thấy người dùng.");
            }

            if (user.IsLocked)
            {
                throw new ConflictException("Tài khoản này đã bị khóa trước đó.");
            }

            user.Lock();
            await userRepository.Update(user);
            return Unit.Value;
        }
    }
}