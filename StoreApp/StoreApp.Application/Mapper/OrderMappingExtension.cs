using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

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
                StaffId: order.StaffId,
                UpdatedAt: order.UpdatedAt,
                OrderStatus: order.OrderStatus,
                Address: order.Address,
                TotalAmount: order.TotalAmount,
                PaymentMethod: order.PaymentMethod
            );
        }

        public static CreateOrderResponseDTO ToCreateOrderResponseDTO(this Order order)
        {
            return new CreateOrderResponseDTO
            (
                Id: order.Id,
                CustomerId: order.CustomerId,
                UpdatedAt: order.UpdatedAt,
                OrderStatus: order.OrderStatus,
                Address: order.Address,
                TotalAmount: order.TotalAmount,
                PaymentMethod: order.PaymentMethod,
                PaymentUrl: null
            );
        }
    }
}
