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
                VoucherCode: order.VoucherCode,

                TotalAmount: order.TotalAmount ?? 0,
                PaymentMethod: order.PaymentMethod,
                Items: order.Items
                    .Select(x => new OrderDetailDTO(
                        x.ProductId,
                        x.Quantity,
                        x.Price,
                        x.Subtotal
                    ))
                    .ToList()
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
                VoucherCode: order.VoucherCode,
                TotalAmount: order.TotalAmount ?? 0,
                PaymentMethod: order.PaymentMethod,
                PaymentUrl: null
            );
        }
    }
}
