using FluentValidation;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Update
{
    public class UpdateCustomerAddressValidator : AbstractValidator<UpdateCustomerAddressCommand>
    {
        public UpdateCustomerAddressValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id địa chỉ không được để trống.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Không lấy được thông tin khách hàng từ token.");

            RuleFor(x => x.ReceiverName)
                .NotEmpty().WithMessage("Tên người nhận không được để trống.")
                .MaximumLength(100).WithMessage("Tên người nhận không được vượt quá 100 ký tự.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được để trống.")
                .Matches(@"^\d{10}$").WithMessage("Số điện thoại phải gồm đúng 10 chữ số.");

            RuleFor(x => x.AddressLine)
                .NotEmpty().WithMessage("Địa chỉ giao hàng không được để trống.")
                .MaximumLength(500).WithMessage("Địa chỉ giao hàng không được vượt quá 500 ký tự.");
        }
    }
}
