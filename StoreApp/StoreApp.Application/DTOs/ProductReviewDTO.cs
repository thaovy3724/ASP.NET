namespace StoreApp.Application.DTOs
{
    public sealed record ProductReviewDTO(
        Guid Id,
        Guid ProductId,
        Guid CustomerId,
        string CustomerName,
        int Rating,
        string? Comment,
        DateTime CreatedAt
    );
}