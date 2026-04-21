using StoreApp.Core.ValueObject;

namespace StoreApp.Application.DTOs
{
    public sealed record OrderDTO(
        Guid Id,
        Guid CustomerId,
        Guid? StaffId,
        Guid? AddressId,
        DateTime UpdatedAt,
        OrderStatus OrderStatus,
        string Address,
        decimal TotalAmount,
        PaymentMethod PaymentMethod,
        List<OrderDetailDTO> Items
    );
}
