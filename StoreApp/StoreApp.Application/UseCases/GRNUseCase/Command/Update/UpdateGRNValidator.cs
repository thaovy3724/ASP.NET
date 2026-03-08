using FluentValidation;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Update
{
    public class UpdateGRNValidator : AbstractValidator<UpdateGRNCommand>
    {
        public UpdateGRNValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Sản phẩm không được để trống")
                .Must(items => items.All(item => item.Quantity > 0)).WithMessage("Số lượng phải lớn hơn 0");
        }
    }
}
