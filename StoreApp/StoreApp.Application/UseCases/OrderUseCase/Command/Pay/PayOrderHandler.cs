using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Pay
{
    public class PayOrderHandler(IOrderRepository orderRepository) : IRequestHandler<PayOrderCommand, Unit>
    {
        public async Task<Unit> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetById(request.Id);
            if (order is null)
            {
                throw new NotFoundException($"Không tìm thấy đơn hàng với Id: {request.Id}");
            }

            order.PayOrder();
            await orderRepository.Update(order);

            return Unit.Value;
        }
    }
}