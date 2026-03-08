using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .Must(items => items.All(item => item.Quantity > 0)).WithMessage("Số lượng phải lớn hơn 0");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum()
                .WithMessage("Phương thức thanh toán không hợp lệ.");
        }
    }
}
