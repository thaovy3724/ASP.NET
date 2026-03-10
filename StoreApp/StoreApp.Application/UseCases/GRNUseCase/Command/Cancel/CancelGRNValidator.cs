using FluentValidation;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Cancel
{
    public class CancelGRNValidator : AbstractValidator<CancelGRNCommand>
    {
        public CancelGRNValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống")
                .NotEqual(Guid.Empty).WithMessage("Id không hợp lệ.");
        }
    }
}
