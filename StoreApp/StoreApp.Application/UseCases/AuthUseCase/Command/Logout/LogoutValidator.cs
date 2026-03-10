using FluentValidation;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.Logout
{
    public class LogoutValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId không được để trống.")
                .NotEqual(Guid.Empty).WithMessage("UserId không hợp lệ.");
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token không được để trống.");
        }
    }
}
