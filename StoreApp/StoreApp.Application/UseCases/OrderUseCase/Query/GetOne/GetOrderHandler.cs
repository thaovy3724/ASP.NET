using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;
using StoreApp.Application.Mapper;
using StoreApp.Application.Exceptions;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.GetOne
{
    public class GetOrderHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderQuery, OrderDTO>
    {
        public async Task<OrderDTO> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            // 1. Lấy dữ liệu từ Repository
            var order = await orderRepository.GetOrderWithDetails(request.Id);

            // 2. Kiểm tra nếu không tìm thấy đơn hàng
            if (order == null)
            {
                throw new NotFoundException($"Không tìm thấy đơn hàng với Id: {request.Id}");
            }

            // 3. Map sang DTO và trả về
            return order.ToDTO();
        }
    }
}
