namespace StoreApp.Application.DTOs
{
    public sealed record ProductDetailDTO(
        Guid Id,
        Guid CategoryId,
        Guid SupplierId,
        string ProductName,
        decimal Price,
        int Quantity,
        DateTime CreatedAt,
        string? ImageUrl,
        double AverageRating,
        int ReviewCount
    );
}