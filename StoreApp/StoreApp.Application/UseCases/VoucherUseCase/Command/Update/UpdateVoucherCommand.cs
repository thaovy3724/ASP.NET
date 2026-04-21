using MediatR;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.VoucherUseCase.Command.Update
{
    public sealed record UpdateVoucherCommand(
        Guid Id,
        string Code,
        decimal DiscountPercent,
        decimal MaxDiscountAmount,
        DateTime StartDate,
        DateTime EndDate,
        int Quantity,
        VoucherStatus Status
    ) : IRequest<Unit>;
}