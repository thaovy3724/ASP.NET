using FluentValidation;

namespace StoreApp.Application.UseCases.CategoryUseCase.Command.BulkDelete
{
    public class BulkDeleteCategoryValidator : AbstractValidator<BulkDeleteCategoryCommand>
    {
        public BulkDeleteCategoryValidator()
        {
            RuleFor(x => x.Ids)
                .NotNull().WithMessage("Danh sách Id không được để trống.")
                .Must(ids => ids != null && ids.Count > 0)
                .WithMessage("Phải chọn ít nhất một danh mục để xóa.");

            RuleForEach(x => x.Ids)
                .NotEmpty().WithMessage("Id trong danh sách không được để trống.")
                .NotEqual(Guid.Empty).WithMessage("Id trong danh sách không hợp lệ.");
        }
    }
}