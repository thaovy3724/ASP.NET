using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.GetList
{
    public class GetListOrderHandler(IOrderRepository orderRepository) : IRequestHandler<GetListOrderQuery, List<OrderDTO>>
    {
        public async Task<List<OrderDTO>> Handle(GetListOrderQuery req, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetListOrderWithDetails();
            var orderDTOs = orders.Select(orders => orders.ToDTO()).ToList();

            return orderDTOs;
        }
    }
}
