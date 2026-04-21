using FluentValidation;

namespace StoreApp.Application.UseCases.VoucherUseCase.Command.Create
{
    public class CreateVoucherValidator : AbstractValidator<CreateVoucherCommand>
    {
        public CreateVoucherValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Mã voucher không được để trống.")
                .MaximumLength(50).WithMessage("Mã voucher tối đa 50 ký tự.");

            RuleFor(x => x.DiscountPercent)
                .GreaterThan(0).WithMessage("Phần trăm giảm giá phải lớn hơn 0.")
                .LessThanOrEqualTo(100).WithMessage("Phần trăm giảm giá không được vượt quá 100.");

            RuleFor(x => x.MaxDiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Số tiền giảm tối đa không được âm.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Số lượng voucher không được âm.");

            RuleFor(x => x)
                .Must(x => x.StartDate < x.EndDate)
                .WithMessage("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
        }
    }
}