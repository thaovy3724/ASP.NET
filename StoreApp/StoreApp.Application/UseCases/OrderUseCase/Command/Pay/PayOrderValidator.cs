using FluentValidation;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Pay
{
    public class PayOrderValidator : AbstractValidator<PayOrderCommand>
    {
        public PayOrderValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống")
                .NotEqual(Guid.Empty).WithMessage("Id không hợp lệ.");
        }
    }
}
