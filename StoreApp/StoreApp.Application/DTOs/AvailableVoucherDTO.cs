namespace StoreApp.Application.DTOs
{
    public sealed record AvailableVoucherDTO(
        string Code,
        decimal DiscountPercent,
        decimal MaxDiscountAmount,
        DateTime EndDate
    );
}