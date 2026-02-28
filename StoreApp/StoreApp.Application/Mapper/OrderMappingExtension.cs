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
                PaymentMethod: order.PaymentMethod, // Nhớ thêm trường này vào Entity Order nếu chưa có, hoặc truyền từ ngoài vào
                // check null (?) và dùng toán tử ?? để luôn trả về list rỗng nếu null
                Items: order.Items?.Select(item => new OrderDetailDTO(
                    ProductId: item.ProductId,
                    Quantity: item.Quantity,
                    Price: item.Price,
                    TotalPrice: item.Subtotal
                )).ToList() ?? new List<OrderDetailDTO>(),
                null // Mặc định PaymentUrl là null khi mới convert
            );
        }
    }
}
