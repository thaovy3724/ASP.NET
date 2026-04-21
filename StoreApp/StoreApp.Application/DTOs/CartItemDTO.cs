namespace StoreApp.Application.DTOs
{
    public sealed record CartItemDTO(
        Guid CartItemId,
        Guid ProductId,
        string ProductName,
        string? ImageUrl,
        decimal Price,
        int Quantity,
        int StockQuantity,
        decimal TotalPrice
    );
}