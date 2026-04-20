using FluentValidation;

namespace StoreApp.Application.UseCases.UserUseCase.Command.BulkDelete
{
    public class BulkDeleteUserValidator : AbstractValidator<BulkDeleteUserCommand>
    {
        public BulkDeleteUserValidator()
        {
            RuleFor(x => x.Ids)
                .NotNull().WithMessage("Danh sách Id không được để trống.")
                .Must(ids => ids != null && ids.Count > 0)
                .WithMessage("Phải chọn ít nhất một người dùng để xóa.");

            RuleForEach(x => x.Ids)
                .NotEmpty().WithMessage("Id người dùng không được để trống.")
                .NotEqual(Guid.Empty).WithMessage("Id người dùng không hợp lệ.");

            RuleFor(x => x.CurrentUserId)
                .NotEqual(Guid.Empty)
                .WithMessage("Không xác định được người dùng đang đăng nhập.");
        }
    }
}