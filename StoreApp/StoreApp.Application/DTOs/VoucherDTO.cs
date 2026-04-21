using StoreApp.Core.ValueObject;

namespace StoreApp.Application.DTOs
{
    public sealed record VoucherDTO(
        Guid Id,
        string Code,
        decimal DiscountPercent,
        decimal MaxDiscountAmount,
        DateTime StartDate,
        DateTime EndDate,
        int Quantity,
        VoucherStatus Status
    );
}