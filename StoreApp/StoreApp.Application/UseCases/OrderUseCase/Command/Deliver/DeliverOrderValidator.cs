using FluentValidation;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Deliver
{
    public class DeliverOrderValidator : AbstractValidator<DeliverOrderCommand>
    {
        public DeliverOrderValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
        }
    }
}
