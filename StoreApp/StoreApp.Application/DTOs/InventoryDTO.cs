namespace StoreApp.Application.DTOs
{
    public sealed record InventoryDTO(
        Guid Id,
        Guid ProductId,
        int Quantity,
        DateTime UpdatedAt
    );
}
