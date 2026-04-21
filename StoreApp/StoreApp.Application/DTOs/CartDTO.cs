namespace StoreApp.Application.DTOs
{
    public sealed record CartDTO(
        Guid Id,
        Guid CustomerId,
        List<CartItemDTO> Items,
        int TotalQuantity,
        decimal TotalAmount
    );
}