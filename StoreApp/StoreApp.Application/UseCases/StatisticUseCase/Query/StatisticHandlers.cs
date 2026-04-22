using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.StatisticUseCase.Query
{
    public class GetDailyRevenueStatisticHandler(IStatisticRepository statisticRepository)
        : IRequestHandler<GetDailyRevenueStatisticQuery, List<DailyRevenueStatisticDTO>>
    {
        public Task<List<DailyRevenueStatisticDTO>> Handle(GetDailyRevenueStatisticQuery request, CancellationToken cancellationToken)
            => statisticRepository.GetDailyRevenueAsync(request.FromDate, request.ToDate, cancellationToken);
    }

    public class GetFinancialStatisticHandler(IStatisticRepository statisticRepository)
        : IRequestHandler<GetFinancialStatisticQuery, List<FinancialStatisticDTO>>
    {
        public Task<List<FinancialStatisticDTO>> Handle(GetFinancialStatisticQuery request, CancellationToken cancellationToken)
            => statisticRepository.GetFinancialStatisticAsync(request.FromDate, request.ToDate, cancellationToken);
    }

    public class GetBestSellingProductsStatisticHandler(IStatisticRepository statisticRepository)
        : IRequestHandler<GetBestSellingProductsStatisticQuery, List<BestSellingProductStatisticDTO>>
    {
        public Task<List<BestSellingProductStatisticDTO>> Handle(GetBestSellingProductsStatisticQuery request, CancellationToken cancellationToken)
            => statisticRepository.GetBestSellingProductsAsync(request.FromDate, request.ToDate, request.Top, cancellationToken);
    }

    public class GetOrderStatusStatisticHandler(IStatisticRepository statisticRepository)
        : IRequestHandler<GetOrderStatusStatisticQuery, List<OrderStatusStatisticDTO>>
    {
        public Task<List<OrderStatusStatisticDTO>> Handle(GetOrderStatusStatisticQuery request, CancellationToken cancellationToken)
            => statisticRepository.GetOrderStatusStatisticAsync(request.FromDate, request.ToDate, cancellationToken);
    }

    public class GetLowStockProductsStatisticHandler(IStatisticRepository statisticRepository)
        : IRequestHandler<GetLowStockProductsStatisticQuery, List<LowStockProductStatisticDTO>>
    {
        public Task<List<LowStockProductStatisticDTO>> Handle(GetLowStockProductsStatisticQuery request, CancellationToken cancellationToken)
            => statisticRepository.GetLowStockProductsAsync(request.Threshold, cancellationToken);
    }

    public class GetPaymentMethodRevenueStatisticHandler(IStatisticRepository statisticRepository)
        : IRequestHandler<GetPaymentMethodRevenueStatisticQuery, List<PaymentMethodRevenueStatisticDTO>>
    {
        public Task<List<PaymentMethodRevenueStatisticDTO>> Handle(GetPaymentMethodRevenueStatisticQuery request, CancellationToken cancellationToken)
            => statisticRepository.GetPaymentMethodRevenueAsync(request.FromDate, request.ToDate, cancellationToken);
    }

    public class GetCategoryRevenueStatisticHandler(IStatisticRepository statisticRepository)
        : IRequestHandler<GetCategoryRevenueStatisticQuery, List<CategoryRevenueStatisticDTO>>
    {
        public Task<List<CategoryRevenueStatisticDTO>> Handle(GetCategoryRevenueStatisticQuery request, CancellationToken cancellationToken)
            => statisticRepository.GetCategoryRevenueAsync(request.FromDate, request.ToDate, cancellationToken);
    }
}