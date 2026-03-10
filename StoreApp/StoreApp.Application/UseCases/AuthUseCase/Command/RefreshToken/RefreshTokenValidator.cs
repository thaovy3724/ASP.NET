using FluentValidation;

namespace StoreApp.Application.UseCases.AuthUseCase.Command.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID không được để trống.");
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Refresh token không được để trống.");
        }
    }
}
