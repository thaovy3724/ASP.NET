using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Cancel
{
    public class CancelOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository) : IRequestHandler<CancelOrderCommand, Unit>
    {
        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetById(request.Id);
            if (order is null)
            {
                throw new NotFoundException($"Không tìm thấy đơn hàng với Id: {request.Id}");
            }

            // Cập nhật trạng thái đơn hàng thành "Đã hủy"
            order.CancelOrder();
            await orderRepository.Update(order);

            // Cập nhật tồn kho cho các sản phẩm trong đơn hàng
            foreach (var item in order.Items)
            {
                var product = await productRepository.GetById(item.ProductId);
                if (product != null)
                {
                    product.IncreaseStock(item.Quantity); // Tăng lại tồn kho
                    await productRepository.Update(product);
                }
            }
            return Unit.Value;

        }
    }
}
