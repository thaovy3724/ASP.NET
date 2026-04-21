using FluentValidation;

namespace StoreApp.Application.UseCases.CartUseCase.Command.DeleteItem
{
    public class DeleteCartItemValidator : AbstractValidator<DeleteCartItemCommand>
    {
        public DeleteCartItemValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Không lấy được thông tin khách hàng từ token.");

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Id sản phẩm không được để trống.");
        }
    }
}