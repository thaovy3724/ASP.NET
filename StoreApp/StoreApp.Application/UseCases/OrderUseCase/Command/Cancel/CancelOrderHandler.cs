using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Cancel
{
    public class CancelOrderHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository
    ) : IRequestHandler<CancelOrderCommand, Unit>
    {
        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByIdWithItems(request.Id);

            if (order is null)
            {
                throw new NotFoundException($"Không tìm thấy đơn hàng với Id: {request.Id}");
            }

            // Nếu là Customer thì chỉ được hủy order của chính mình
            if (request.CustomerId.HasValue && order.CustomerId != request.CustomerId.Value)
            {
                throw new ForbiddenException("Bạn không có quyền hủy đơn hàng này.");
            }

            order.CancelOrder(request.StaffId);
            await orderRepository.Update(order);

            foreach (var item in order.Items)
            {
                var product = await productRepository.GetById(item.ProductId);

                if (product != null)
                {
                    product.IncreaseStock(item.Quantity);
                    await productRepository.Update(product);
                }
            }

            return Unit.Value;
        }
    }
}