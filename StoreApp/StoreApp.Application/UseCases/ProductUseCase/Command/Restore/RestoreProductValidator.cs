using FluentValidation;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Restore
{
    public class RestoreProductValidator : AbstractValidator<RestoreProductCommand>
    {
        public RestoreProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id sản phẩm không được rỗng.");
        }
    }
}