using FluentValidation;

namespace StoreApp.Application.UseCases.CartUseCase.Command.Clear
{
    public class ClearCartValidator : AbstractValidator<ClearCartCommand>
    {
        public ClearCartValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Không lấy được thông tin khách hàng từ token.");
        }
    }
}