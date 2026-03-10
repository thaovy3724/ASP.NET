using FluentValidation;

namespace StoreApp.Application.UseCases.OrderUseCase.Query.GetOne
{
    public class GetOrderValidator : AbstractValidator<GetOrderQuery>
    {
        public GetOrderValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống")
                .NotEqual(Guid.Empty).WithMessage("Id không hợp lệ.");
        }
    }
}
