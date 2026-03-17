using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.GetList
{
    public class GetListOrderHandler(IOrderRepository orderRepository) : IRequestHandler<GetListOrderQuery, PagedList<OrderDTO>>
    {
        public async Task<PagedList<OrderDTO>> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
        {
            //var result = await orderRepository.GetListOrderWithDetails();
            if (request.CustomerId == null)
            {
                var result = await orderRepository.Search(request.PageNumber, request.PageSize);
                var orderListDTO = result.Items.
                    Select(orders => orders.ToDTO())
                    .ToList();
                return new PagedList<OrderDTO>(orderListDTO, result.MetaData);
            }
            else
            {
                var result = await orderRepository.Search(request.PageNumber, request.PageSize, request.CustomerId);
                var orderListDTO = result.Items.
                    Select(orders => orders.ToDTO())
                    .ToList();
                return new PagedList<OrderDTO>(orderListDTO, result.MetaData);
            }
        }
    }
}
