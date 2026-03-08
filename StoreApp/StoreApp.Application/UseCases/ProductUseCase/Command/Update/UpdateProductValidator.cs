using FluentValidation;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Update
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
            RuleFor(x => x.CategoryId)
                 .NotEmpty().WithMessage("Thể loại không được để trống");
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Tên sản phẩm không được để trống");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Giá sản phẩm phải lớn hơn 0");
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Hình ảnh sản phẩm không được để trống");
        }
    }
}
