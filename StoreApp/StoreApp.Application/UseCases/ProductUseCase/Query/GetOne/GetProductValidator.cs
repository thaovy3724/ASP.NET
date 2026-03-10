using FluentValidation;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetOne
{
    public class GetProductValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống")
                .NotEqual(Guid.Empty).WithMessage("Id không hợp lệ.");
        }
    }
}
