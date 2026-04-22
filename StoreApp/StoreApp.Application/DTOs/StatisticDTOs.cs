namespace StoreApp.Application.DTOs
{
    public sealed record DailyRevenueStatisticDTO(
        DateTime Date,
        decimal TotalRevenue,
        int OrderCount
    );

    public sealed record BestSellingProductStatisticDTO(
        Guid ProductId,
        string ProductName,
        int TotalQuantitySold,
        decimal TotalRevenue
    );

    public sealed record OrderStatusStatisticDTO(
        string Status,
        int Count
    );

    public sealed record LowStockProductStatisticDTO(
        Guid ProductId,
        string ProductName,
        int Quantity
    );

    public sealed record PaymentMethodRevenueStatisticDTO(
        string PaymentMethod,
        decimal TotalRevenue,
        int OrderCount
    );

    public sealed record CategoryRevenueStatisticDTO(
        Guid CategoryId,
        string CategoryName,
        int TotalQuantitySold,
        decimal TotalRevenue
    );
}