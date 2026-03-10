using FluentValidation;

namespace StoreApp.Application.UseCases.GRNUseCase.Query.GetOne
{
    public class GetGRNValidator : AbstractValidator<GetGRNQuery>
    {
        public GetGRNValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống")
                .NotEqual(Guid.Empty).WithMessage("Id không hợp lệ.");
        }
    }
}
