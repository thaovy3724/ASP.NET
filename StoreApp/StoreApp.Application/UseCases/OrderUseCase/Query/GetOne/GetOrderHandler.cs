using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.GetOne
{
    public class GetOrderHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderQuery, ResultWithData<OrderDTO>>
    {
        public async Task<ResultWithData<OrderDTO>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            // 1. Lấy dữ liệu từ Repository
            var order = await orderRepository.GetOrderWithDetails(request.Id);

            // 2. Kiểm tra nếu không tìm thấy đơn hàng
            if (order == null)
            {
                throw new NotFoundException($"Không tìm thấy đơn hàng với Id: {request.Id}");
            }

            // 3. Map sang DTO và trả về
            return new ResultWithData<OrderDTO>(
                Success: true,
                Message: "Lấy thông tin đơn hàng thành công.",
                Data: order.ToDTO()
            );
        }
    }
}
