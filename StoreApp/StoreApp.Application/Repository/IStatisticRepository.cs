using StoreApp.Application.DTOs;

namespace StoreApp.Application.Repository
{
    public interface IStatisticRepository
    {
        Task<List<DailyRevenueStatisticDTO>> GetDailyRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default);

        Task<List<FinancialStatisticDTO>> GetFinancialStatisticAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default);

        Task<List<BestSellingProductStatisticDTO>> GetBestSellingProductsAsync(
            DateTime fromDate,
            DateTime toDate,
            int top,
            CancellationToken cancellationToken = default);

        Task<List<OrderStatusStatisticDTO>> GetOrderStatusStatisticAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default);

        Task<List<LowStockProductStatisticDTO>> GetLowStockProductsAsync(
            int threshold,
            CancellationToken cancellationToken = default);

        Task<List<PaymentMethodRevenueStatisticDTO>> GetPaymentMethodRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default);

        Task<List<CategoryRevenueStatisticDTO>> GetCategoryRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default);
    }
}