using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Confirm
{
    public class ConfirmOrderHandler(IOrderRepository orderRepository) : IRequestHandler<ConfirmOrderCommand, Result>
    {
        public async Task<Result> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetById(request.Id);
            if (order is null)
            {
                throw new NotFoundException($"Không tìm thấy đơn hàng với Id: {request.Id}");
            }

            order.MarkAsConfirmed();
            await orderRepository.Update(order);
            return new Result { Success = true, Message = "Xác nhận đơn hàng thành công" };
        }
    }
}
