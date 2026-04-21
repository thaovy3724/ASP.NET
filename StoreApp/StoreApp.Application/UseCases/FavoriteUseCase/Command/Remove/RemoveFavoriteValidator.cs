using FluentValidation;

namespace StoreApp.Application.UseCases.FavoriteUseCase.Command.Remove
{
    public class RemoveFavoriteValidator : AbstractValidator<RemoveFavoriteCommand>
    {
        public RemoveFavoriteValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Id khách hàng không được để trống.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Id sản phẩm không được để trống.");
        }
    }
}