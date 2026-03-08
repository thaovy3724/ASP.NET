using FluentValidation;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Delete
{
    public class DeleteSupplierValidator : AbstractValidator<DeleteSupplierCommand>
    {
        public DeleteSupplierValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id không được để trống");
        }
    }
}
