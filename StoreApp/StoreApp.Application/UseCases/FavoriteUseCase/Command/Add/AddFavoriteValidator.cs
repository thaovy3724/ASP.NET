using FluentValidation;

namespace StoreApp.Application.UseCases.FavoriteUseCase.Command.Add
{
    public class AddFavoriteValidator : AbstractValidator<AddFavoriteCommand>
    {
        public AddFavoriteValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Id khách hàng không được để trống.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Id sản phẩm không được để trống.");
        }
    }
}