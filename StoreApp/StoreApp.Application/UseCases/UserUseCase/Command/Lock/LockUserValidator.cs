using FluentValidation;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Lock
{
    public class LockUserValidator : AbstractValidator<LockUserCommand>
    {
        public LockUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id người dùng cần khóa không được rỗng.");

            RuleFor(x => x.CurrentUserId)
                .NotEmpty().WithMessage("Không xác định được người dùng đang đăng nhập.");
        }
    }
}