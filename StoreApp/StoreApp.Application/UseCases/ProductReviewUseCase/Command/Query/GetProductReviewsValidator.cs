using FluentValidation;

namespace StoreApp.Application.UseCases.ProductReviewUseCase.Query.GetByProduct
{
    public class GetProductReviewsValidator : AbstractValidator<GetProductReviewsQuery>
    {
        public GetProductReviewsValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Id sản phẩm không được để trống");
        }
    }
}