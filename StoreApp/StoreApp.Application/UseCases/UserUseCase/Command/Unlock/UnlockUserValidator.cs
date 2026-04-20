using FluentValidation;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Unlock
{
    public class UnlockUserValidator : AbstractValidator<UnlockUserCommand>
    {
        public UnlockUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id người dùng cần mở khóa không được rỗng.");
        }
    }
}