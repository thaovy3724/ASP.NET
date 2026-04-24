using System;
using System.Linq;
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

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Địa chỉ không được để trống");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Sản phẩm không được để trống")
                .Must(items => items.All(item => item.Quantity > 0 && item.Price > 0)).
                WithMessage("Tất cả sản phẩm phải có số lượng và đơn giá lớn hơn 0");

            RuleFor(x => x.PaymentMethod)
                .Must(BeAValidPaymentMethod)
                .WithMessage("Phương thức thanh toán không hợp lệ");

            
        }

        // Hàm phụ để kiểm tra chuỗi có thuộc Enum không
        private bool BeAValidPaymentMethod(string method)
        {
            return Enum.TryParse(typeof(PaymentMethod), method, true, out _);
        }
    }
}
