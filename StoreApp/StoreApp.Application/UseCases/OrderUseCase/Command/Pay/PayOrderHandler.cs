using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.UseCases.OrderUseCase.Command.Cancel;


namespace StoreApp.Application.UseCases.OrderUseCase.Command.Pay
{
    public class PayOrderHandler(IOrderRepository orderRepository) : IRequestHandler<CancelOrderCommand, Unit>
    {
        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
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
