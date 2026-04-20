using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Unlock
{
    public class UnlockUserHandler(IUserRepository userRepository) : IRequestHandler<UnlockUserCommand, Unit>
    {
        public async Task<Unit> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetById(request.Id);
            if (user is null)
            {
                throw new NotFoundException("Không tìm thấy người dùng.");
            }

            if (!user.IsLocked)
            {
                throw new ConflictException("Tài khoản này đang hoạt động, không cần mở khóa.");
            }

            user.Unlock();
            await userRepository.Update(user);
            return Unit.Value;
        }
    }
}