using FluentValidation;

namespace StoreApp.Application.UseCases.CustomerAddressUseCase.Command.Delete
{
    public class DeleteCustomerAddressValidator : AbstractValidator<DeleteCustomerAddressCommand>
    {
        public DeleteCustomerAddressValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id địa chỉ không được để trống.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Không lấy được thông tin khách hàng từ token.");
        }
    }
}
