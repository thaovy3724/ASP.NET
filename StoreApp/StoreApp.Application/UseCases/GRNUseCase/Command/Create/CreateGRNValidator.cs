using FluentValidation;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Create
{
    public class CreateGRNValidator : AbstractValidator<CreateGRNCommand>
    {
        public CreateGRNValidator()
        {
            RuleFor(x => x.SupplierId)
                .NotEmpty().WithMessage("Id nhà cung cấp không được để trống");
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Sản phẩm không được để trống")
                .Must(items => items.All(item => item.Quantity > 0 && item.Price > 0))
                .WithMessage("Tất cả sản phẩm phải có số lượng và đơn giá lớn hơn 0");
        }
    }
}
