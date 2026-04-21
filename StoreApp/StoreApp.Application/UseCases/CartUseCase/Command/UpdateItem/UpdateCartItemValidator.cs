using FluentValidation;

namespace StoreApp.Application.UseCases.CartUseCase.Command.UpdateItem
{
    public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemCommand>
    {
        public UpdateCartItemValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Không lấy được thông tin khách hàng từ token.");

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Id sản phẩm không được để trống.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Số lượng phải lớn hơn 0.");
        }
    }
}