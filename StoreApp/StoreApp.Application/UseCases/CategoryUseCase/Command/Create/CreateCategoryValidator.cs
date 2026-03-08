using FluentValidation;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.Create
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(100).WithMessage("Tên không được quá 100 kí tự");
        }
    }
}
