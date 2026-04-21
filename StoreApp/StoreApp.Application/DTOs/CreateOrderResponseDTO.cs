using StoreApp.Core.ValueObject;

namespace StoreApp.Application.DTOs
{
    public sealed record CreateOrderResponseDTO(
        Guid Id,
        Guid CustomerId,
        Guid? AddressId,
        DateTime UpdatedAt,
        OrderStatus OrderStatus,
        string Address,
        decimal TotalAmount,
        PaymentMethod PaymentMethod,
        string? PaymentUrl);
}
