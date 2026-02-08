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
                TotalAmount: order.Items.Sum(item => item.Subtotal),
                Items: order.Items.Select(static item => item.ToDTO()).ToList()
            );
        }

        public static Order ToEntity(this OrderDTO orderDTO)
        {
            return new Order
            (
                customerId: orderDTO.CustomerId,
                userId: orderDTO.UserId,
                promoId: orderDTO.PromoId,
                orderDate: orderDTO.OrderDate,
                orderStatus: orderDTO.OrderStatus,
                discountAmount: orderDTO.DiscountAmount
            );
        }
    }
}
