namespace StoreApp.Application.DTOs
{
    public sealed record ProductDTO(
        Guid Id,
        Guid CategoryId,
        Guid SupplierId,
        string ProductName,
        string Barcode,
        decimal Price,
        string Unit,
        string? ImageUrl
    );
}
