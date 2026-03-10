using FluentValidation;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Thể loại không được để trống");
            RuleFor(x => x.SupplierId)
                .NotEmpty().WithMessage("Nhà cung cấp không được để trống");
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Tên sản phẩm không được để trống");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Giá sản phẩm phải lớn hơn 0");
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Hình ảnh sản phẩm không được để trống");
        }
    }
}
