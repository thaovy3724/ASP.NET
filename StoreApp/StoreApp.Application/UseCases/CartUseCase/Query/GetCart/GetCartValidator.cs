using FluentValidation;

namespace StoreApp.Application.UseCases.CartUseCase.Query.GetCart
{
    public class GetCartValidator : AbstractValidator<GetCartQuery>
    {
        public GetCartValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Không lấy được thông tin khách hàng từ token.");
        }
    }
}