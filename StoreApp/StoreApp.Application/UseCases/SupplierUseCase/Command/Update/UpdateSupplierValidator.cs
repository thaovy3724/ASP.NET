using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Update
{
    public class UpdateSupplierValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
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
