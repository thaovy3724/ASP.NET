namespace StoreApp.Application.DTOs
{
    public sealed record FinancialStatisticDTO(
        DateTime Date,
        decimal OrderRevenue,
        decimal GRNCost,
        decimal NetIncome,
        int OrderCount
    );
}