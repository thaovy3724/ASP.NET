using FluentValidation;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.BulkDelete
{
    public class BulkDeleteGRNValidator : AbstractValidator<BulkDeleteGRNCommand>
    {
        public BulkDeleteGRNValidator()
        {
            RuleFor(x => x.Ids)
                .NotNull().WithMessage("Danh sách Id không được để trống.")
                .Must(ids => ids != null && ids.Count > 0)
                .WithMessage("Phải chọn ít nhất một phiếu nhập để xóa.");

            RuleForEach(x => x.Ids)
                .NotEmpty().WithMessage("Id phiếu nhập không được để trống.")
                .NotEqual(Guid.Empty).WithMessage("Id phiếu nhập không hợp lệ.");
        }
    }
}