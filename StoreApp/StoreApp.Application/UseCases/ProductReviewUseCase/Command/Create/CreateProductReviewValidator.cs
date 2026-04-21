using FluentValidation;

namespace StoreApp.Application.UseCases.ProductReviewUseCase.Command.Create
{
    public class CreateProductReviewValidator : AbstractValidator<CreateProductReviewCommand>
    {
        public CreateProductReviewValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Id sản phẩm không được để trống");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Id khách hàng không được để trống");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Số sao đánh giá phải từ 1 đến 5");

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage("Nội dung đánh giá không được vượt quá 500 ký tự");
        }
    }
}