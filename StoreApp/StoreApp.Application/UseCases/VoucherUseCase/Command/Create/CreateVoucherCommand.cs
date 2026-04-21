using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.VoucherUseCase.Command.Create
{
    public sealed record CreateVoucherCommand(
        string Code,
        decimal DiscountPercent,
        decimal MaxDiscountAmount,
        DateTime StartDate,
        DateTime EndDate,
        int Quantity,
        VoucherStatus Status
    ) : IRequest<VoucherDTO>;
}