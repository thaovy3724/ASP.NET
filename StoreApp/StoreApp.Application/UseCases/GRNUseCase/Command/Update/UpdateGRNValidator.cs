using FluentValidation;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Update
{
    public class UpdateGRNValidator : AbstractValidator<UpdateGRNCommand>
    {
        public UpdateGRNValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống")
                .NotEqual(Guid.Empty).WithMessage("Id không hợp lệ.");
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Sản phẩm không được để trống")
                .Must(items => items.All(item => item.Quantity > 0 && item.Price > 0))
                .WithMessage("Tất cả sản phẩm phải có số lượng và đơn giá lớn hơn 0");
        }
    }
}
