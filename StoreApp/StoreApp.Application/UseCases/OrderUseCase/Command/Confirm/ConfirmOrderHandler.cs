using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Confirm
{
    public class ConfirmOrderHandler(IOrderRepository orderRepository) : IRequestHandler<ConfirmOrderCommand, Unit>
    {
        public async Task<Unit> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetById(request.Id);
            if (order is null)
            {
                throw new NotFoundException($"Không tìm thấy đơn hàng với Id: {request.Id}");
            }

            order.ConfirmOrder(request.StaffId);
            await orderRepository.Update(order);
            return Unit.Value;
        }
    }
}
