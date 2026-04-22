using FluentValidation;

namespace StoreApp.Application.UseCases.StatisticUseCase.Query.GetFinancial
{
    public class GetFinancialStatisticValidator : AbstractValidator<GetFinancialStatisticQuery>
    {
        public GetFinancialStatisticValidator()
        {
            RuleFor(x => x.FromDate)
                .NotEmpty().WithMessage("Từ ngày không được để trống.");

            RuleFor(x => x.ToDate)
                .NotEmpty().WithMessage("Đến ngày không được để trống.");

            RuleFor(x => x)
                .Must(x => x.FromDate.Date <= x.ToDate.Date)
                .WithMessage("Từ ngày không được lớn hơn đến ngày.");
        }
    }
}