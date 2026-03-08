using FluentValidation;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Complete
{
    public class CompleteGRNValidator : AbstractValidator<CompleteGRNCommand>
    {
        public CompleteGRNValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
        }
    }
}
