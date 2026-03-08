using FluentValidation;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Update
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator() {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống")
                .EmailAddress().WithMessage("Tên đăng nhập phải là một địa chỉ email hợp lệ");
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ tên không được để trống")
                .MaximumLength(100).WithMessage("Họ tên không được quá 100 kí tự");
            RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Vai trò không hợp lệ");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được để trống")
                .Matches(@"^\d{10}$").WithMessage("Số điện thoại phải có 10 chữ số");
        }
    }
}
