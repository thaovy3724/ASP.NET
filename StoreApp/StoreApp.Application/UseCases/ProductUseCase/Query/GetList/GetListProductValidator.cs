using FluentValidation;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetList
{
    public class GetListProductValidator : AbstractValidator<GetListProductQuery>
    {
        public GetListProductValidator()
        {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinPrice.HasValue)
                .WithMessage("Giá nhỏ nhất không được âm.");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxPrice.HasValue)
                .WithMessage("Giá lớn nhất không được âm.");

            RuleFor(x => x)
                .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice)
                .WithMessage("MinPrice phải nhỏ hơn hoặc bằng MaxPrice.");

            RuleFor(x => x.MinQuantity)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinQuantity.HasValue)
                .WithMessage("Số lượng nhỏ nhất không được âm.");

            RuleFor(x => x.MaxQuantity)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaxQuantity.HasValue)
                .WithMessage("Số lượng lớn nhất không được âm.");

            RuleFor(x => x)
                .Must(x => !x.MinQuantity.HasValue || !x.MaxQuantity.HasValue || x.MinQuantity <= x.MaxQuantity)
                .WithMessage("MinQuantity phải nhỏ hơn hoặc bằng MaxQuantity.");

            RuleFor(x => x.SortBy)
                .Must(IsValidSortBy)
                .WithMessage("SortBy chỉ được là CreatedAt, ProductName, Price hoặc Quantity.");
        }

        private static bool IsValidSortBy(string? sortBy)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return true;

            var allowed = new[] { "CreatedAt", "ProductName", "Price", "Quantity" };

            return allowed.Any(x =>
                string.Equals(x, sortBy.Trim(), StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}