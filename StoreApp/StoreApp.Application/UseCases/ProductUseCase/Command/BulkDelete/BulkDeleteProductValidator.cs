using FluentValidation;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.BulkDelete
{
    public class BulkDeleteProductValidator : AbstractValidator<BulkDeleteProductCommand>
    {
        public BulkDeleteProductValidator()
        {
            RuleFor(x => x.Ids)
                .NotNull().WithMessage("Danh sách Id không được để trống.")
                .Must(ids => ids != null && ids.Count > 0)
                .WithMessage("Phải chọn ít nhất một sản phẩm để xóa.");

            RuleForEach(x => x.Ids)
                .NotEmpty().WithMessage("Id sản phẩm không được để trống.")
                .NotEqual(Guid.Empty).WithMessage("Id sản phẩm không hợp lệ.");
        }
    }
}