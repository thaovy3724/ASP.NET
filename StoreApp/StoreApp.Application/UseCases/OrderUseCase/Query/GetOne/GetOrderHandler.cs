using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;
using StoreApp.Application.Mapper;
using StoreApp.Application.Exceptions;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.GetOne
{
    public class GetOrderHandler(IOrderRepository orderRepository)
        : IRequestHandler<GetOrderQuery, OrderDTO>
    {
        public async Task<OrderDTO> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByIdWithItems(request.Id);

            if (order == null)
            {
                throw new NotFoundException($"Không tìm thấy đơn hàng với Id: {request.Id}");
            }

            // Nếu là Customer thì chỉ được xem order của chính mình
            if (request.CustomerId.HasValue && order.CustomerId != request.CustomerId.Value)
            {
                throw new ForbiddenException("Bạn không có quyền xem đơn hàng này.");
            }

            return order.ToDTO();
        }
    }
}