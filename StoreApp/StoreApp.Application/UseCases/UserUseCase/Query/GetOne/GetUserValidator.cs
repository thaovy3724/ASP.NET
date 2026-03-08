using FluentValidation;

namespace StoreApp.Application.UseCases.UserUseCase.Query.GetOne
{
    public class GetUserValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
        }
    }
}
