using FluentValidation;

namespace StoreApp.Application.UseCases.FavoriteUseCase.Query.GetList
{
    public class GetListFavoriteValidator : AbstractValidator<GetListFavoriteQuery>
    {
        public GetListFavoriteValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Id khách hàng không được để trống.");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber phải lớn hơn 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("PageSize phải từ 1 đến 100.");
        }
    }
}