using FluentValidation;

namespace StoreApp.Application.UseCases.UserUseCase.Command.Create
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator() { 
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống")
                .EmailAddress().WithMessage("Tên đăng nhập phải là một địa chỉ email hợp lệ");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự");
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
