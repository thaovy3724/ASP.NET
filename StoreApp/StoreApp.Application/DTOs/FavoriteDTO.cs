namespace StoreApp.Application.DTOs
{
    public sealed record FavoriteDTO(
        Guid Id,
        Guid ProductId,
        string ProductName,
        decimal Price,
        int Quantity,
        string ImageUrl,
        DateTime CreatedAt
    );
}