using FluentValidation;

namespace StoreApp.Application.UseCases.StatisticUseCase.Query
{
    public class GetDailyRevenueStatisticValidator : AbstractValidator<GetDailyRevenueStatisticQuery>
    {
        public GetDailyRevenueStatisticValidator()
        {
            RuleFor(x => x)
                .Must(x => x.FromDate.Date <= x.ToDate.Date)
                .WithMessage("Từ ngày không được lớn hơn đến ngày.");
        }
    }

    public class GetFinancialStatisticValidator : AbstractValidator<GetFinancialStatisticQuery>
    {
        public GetFinancialStatisticValidator()
        {
            RuleFor(x => x)
                .Must(x => x.FromDate.Date <= x.ToDate.Date)
                .WithMessage("Từ ngày không được lớn hơn đến ngày.");
        }
    }

    public class GetBestSellingProductsStatisticValidator : AbstractValidator<GetBestSellingProductsStatisticQuery>
    {
        public GetBestSellingProductsStatisticValidator()
        {
            RuleFor(x => x)
                .Must(x => x.FromDate.Date <= x.ToDate.Date)
                .WithMessage("Từ ngày không được lớn hơn đến ngày.");

            RuleFor(x => x.Top)
                .InclusiveBetween(1, 100)
                .WithMessage("Top phải từ 1 đến 100.");
        }
    }

    public class GetOrderStatusStatisticValidator : AbstractValidator<GetOrderStatusStatisticQuery>
    {
        public GetOrderStatusStatisticValidator()
        {
            RuleFor(x => x)
                .Must(x => x.FromDate.Date <= x.ToDate.Date)
                .WithMessage("Từ ngày không được lớn hơn đến ngày.");
        }
    }

    public class GetLowStockProductsStatisticValidator : AbstractValidator<GetLowStockProductsStatisticQuery>
    {
        public GetLowStockProductsStatisticValidator()
        {
            RuleFor(x => x.Threshold)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ngưỡng tồn kho không được âm.");
        }
    }

    public class GetPaymentMethodRevenueStatisticValidator : AbstractValidator<GetPaymentMethodRevenueStatisticQuery>
    {
        public GetPaymentMethodRevenueStatisticValidator()
        {
            RuleFor(x => x)
                .Must(x => x.FromDate.Date <= x.ToDate.Date)
                .WithMessage("Từ ngày không được lớn hơn đến ngày.");
        }
    }

    public class GetCategoryRevenueStatisticValidator : AbstractValidator<GetCategoryRevenueStatisticQuery>
    {
        public GetCategoryRevenueStatisticValidator()
        {
            RuleFor(x => x)
                .Must(x => x.FromDate.Date <= x.ToDate.Date)
                .WithMessage("Từ ngày không được lớn hơn đến ngày.");
        }
    }
}