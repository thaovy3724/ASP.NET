using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.DTOs
{
    public sealed record CreateOrderResponseDTO(
        Guid Id,
        Guid CustomerId,
        DateTime UpdatedAt,
        OrderStatus OrderStatus,
        string Address,
        Guid? VoucherCode,
        decimal TotalAmount,
        PaymentMethod PaymentMethod,
        string? PaymentUrl); // Nếu PaymentMethod = Cash thì PaymentUrl = null
}
