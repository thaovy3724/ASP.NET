namespace StoreApp.Application.DTOs
{
    public sealed record GRNDetailDTO(Guid ProductId, int Quantity, decimal Price);
}
