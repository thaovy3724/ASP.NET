using FluentValidation;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Id khách hàng không được để trống");

            RuleFor(x => x)
                .Must(x => x.AddressId.HasValue || !string.IsNullOrWhiteSpace(x.Address))
                .WithMessage("Vui lòng chọn địa chỉ giao hàng hoặc nhập địa chỉ giao hàng.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Sản phẩm không được để trống")
                .Must(items => items.All(item => item.Quantity > 0 && item.Price > 0))
                .WithMessage("Tất cả sản phẩm phải có số lượng và đơn giá lớn hơn 0");

            RuleFor(x => x.PaymentMethod)
                .Must(BeAValidPaymentMethod)
                .WithMessage("Phương thức thanh toán không hợp lệ");
        }

        private bool BeAValidPaymentMethod(string method)
        {
            return Enum.TryParse(typeof(PaymentMethod), method, true, out _);
        }
    }
}
