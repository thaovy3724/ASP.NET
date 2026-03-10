using FluentValidation;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Create
{
    public class CreateSupplierValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên nhà cung cấp không được để trống");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được để trống")
                .Matches(@"^\d{10}$").WithMessage("Số điện thoại phải có 10 chữ số");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không hợp lệ");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Địa chỉ không được để trống");
        }
    }
}
