using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.GetList
{
    public class GetListOrderHandler(IOrderRepository orderRepository) : IRequestHandler<GetListOrderQuery, ResultWithData<List<OrderDTO>>>
    {
        public async Task<ResultWithData<List<OrderDTO>>> Handle(GetListOrderQuery req, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetAll();
            var orderDTOs = orders.Select(orders => orders.ToDTO()).ToList();

            return new ResultWithData<List<OrderDTO>>(
                Success: true,
                Message: "Lấy danh sách đơn hàng thành công",
                Data: orderDTOs
                );
        }
    }
}
