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

namespace StoreApp.Application.UseCases.OrderUseCase.Query.Search
{
    public class SearchOrderHandler(IOrderRepository orderRepository) : IRequestHandler<SearchOrderQuery, ResultWithData<List<OrderDTO>>>
    {
        public async Task<ResultWithData<List<OrderDTO>>> Handle(SearchOrderQuery request, CancellationToken cancellationToken)
        {
            // 1. Gọi Repository để lấy dữ liệu (Đã có logic Filter bên trong Repository)
            var orders = await orderRepository.Search(request.keyword);

            // 2. Map từ Entity sang DTO bằng cách dùng Extension Method ToDTO()
            var orderDTOs = orders.Select(order => order.ToDTO()).ToList();

            // 3. Trả về kết quả
            return new ResultWithData<List<OrderDTO>>
            (
                Success: true,
                Message: $"Tìm thấy {orderDTOs.Count} đơn hàng.",
                Data: orderDTOs
            );
        }
    }
}
