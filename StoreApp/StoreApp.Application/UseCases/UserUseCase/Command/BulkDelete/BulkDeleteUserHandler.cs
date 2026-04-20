using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.UserUseCase.Command.BulkDelete
{
    public class BulkDeleteUserHandler(
        IUserRepository userRepository,
        IOrderRepository orderRepository
    ) : IRequestHandler<BulkDeleteUserCommand, Unit>
    {
        public async Task<Unit> Handle(BulkDeleteUserCommand request, CancellationToken cancellationToken)
        {
            var ids = request.Ids?
                .Where(id => id != Guid.Empty)
                .Distinct()
                .ToList() ?? [];

            if (ids.Count == 0)
            {
                throw new BadRequestException("Phải chọn ít nhất một người dùng để xóa.");
            }

            if (ids.Contains(request.CurrentUserId))
            {
                throw new BadRequestException("Bạn không thể tự xóa chính mình khi đang đăng nhập.");
            }

            var users = await userRepository.GetByIds(ids);

            var foundIds = users.Select(x => x.Id).ToHashSet();
            var missingIds = ids.Where(id => !foundIds.Contains(id)).ToList();

            if (missingIds.Count > 0)
            {
                throw new NotFoundException("Có người dùng không tồn tại.");
            }

            foreach (var user in users)
            {
                if (await orderRepository.IsExist(o => o.CustomerId == user.Id || o.StaffId == user.Id))
                {
                    throw new ConflictException($"Người dùng \"{user.Username}\" đang có thông tin đơn hàng, không thể xóa.");
                }
            }

            await userRepository.DeleteRange(users);

            return Unit.Value;
        }
    }
}