using FluentValidation;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Confirm
{
    public class ConfirmOrderValidator : AbstractValidator<ConfirmOrderCommand>
    {
        public ConfirmOrderValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống")
                .NotEqual(Guid.Empty).WithMessage("Id không hợp lệ.");
        }
    }
}
