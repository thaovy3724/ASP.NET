using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Deliver
{
    public class DeliverOrderHandler(IOrderRepository orderRepository) : IRequestHandler<DeliverOrderCommand, Unit>
    {
        public async Task<Unit> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetById(request.Id);
            if (order is null)
            {
                throw new NotFoundException($"Không tìm thấy đơn hàng với Id: {request.Id}");
            }

            order.MarkAsDelivered();
            await orderRepository.Update(order);
            return Unit.Value;
        }
    }
}
