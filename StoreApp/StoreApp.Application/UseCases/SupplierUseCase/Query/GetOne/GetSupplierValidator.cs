using FluentValidation;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetOne
{
    public class GetSupplierValidator : AbstractValidator<GetSupplierQuery>
    {
        public GetSupplierValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
        }
    }
}
