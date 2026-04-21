using FluentValidation;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Không lấy được thông tin khách hàng từ token.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Địa chỉ không được để trống.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty()
                .WithMessage("Phương thức thanh toán không được để trống.")
                .Must(BeAValidPaymentMethod)
                .WithMessage("Phương thức thanh toán không hợp lệ.");
        }

        private bool BeAValidPaymentMethod(string method)
        {
            return Enum.TryParse(typeof(PaymentMethod), method, true, out _);
        }
    }
}