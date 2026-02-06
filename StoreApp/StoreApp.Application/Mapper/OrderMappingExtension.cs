using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Mapper
{
    public static class OrderMappingExtension
    {
        public static OrderDTO ToDTO(this Order order)
        {
            return new OrderDTO
            (
                Id: order.Id,
                CustomerId: order.CustomerId,
                UserId: order.UserId,
                PromoId: order.PromoId,
                OrderDate: order.OrderDate,
                OrderStatus: order.OrderStatus,
                DiscountAmount: order.DiscountAmount,
                TotalAmount: order.TotalAmount,

                // check null (?) và dùng toán tử ?? để luôn trả về list rỗng nếu null
                Items: order.Items?.Select(item => new OrderItemDTO(
                    ProductId: item.ProductId,
                    Quantity: item.Quantity,
                    UnitPrice: item.Price,
                    TotalPrice: item.Subtotal
                )).ToList() ?? new List<OrderItemDTO>()
            );
        }
    }
}
