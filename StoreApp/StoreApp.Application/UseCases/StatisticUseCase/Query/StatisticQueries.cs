using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.StatisticUseCase.Query
{
    public sealed record GetDailyRevenueStatisticQuery(DateTime FromDate, DateTime ToDate)
        : IRequest<List<DailyRevenueStatisticDTO>>;

    public sealed record GetFinancialStatisticQuery(DateTime FromDate, DateTime ToDate)
        : IRequest<List<FinancialStatisticDTO>>;

    public sealed record GetBestSellingProductsStatisticQuery(DateTime FromDate, DateTime ToDate, int Top)
        : IRequest<List<BestSellingProductStatisticDTO>>;

    public sealed record GetOrderStatusStatisticQuery(DateTime FromDate, DateTime ToDate)
        : IRequest<List<OrderStatusStatisticDTO>>;

    public sealed record GetLowStockProductsStatisticQuery(int Threshold)
        : IRequest<List<LowStockProductStatisticDTO>>;

    public sealed record GetPaymentMethodRevenueStatisticQuery(DateTime FromDate, DateTime ToDate)
        : IRequest<List<PaymentMethodRevenueStatisticDTO>>;

    public sealed record GetCategoryRevenueStatisticQuery(DateTime FromDate, DateTime ToDate)
        : IRequest<List<CategoryRevenueStatisticDTO>>;
}