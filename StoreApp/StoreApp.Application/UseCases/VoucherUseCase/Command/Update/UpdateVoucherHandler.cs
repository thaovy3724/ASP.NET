using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.VoucherUseCase.Command.Update
{
    public class UpdateVoucherHandler(IVoucherRepository voucherRepository)
        : IRequestHandler<UpdateVoucherCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
        {
            var voucher = await voucherRepository.GetById(request.Id);
            if (voucher is null)
            {
                throw new NotFoundException("Voucher không tồn tại.");
            }

            if (await voucherRepository.IsCodeExist(request.Code, request.Id))
            {
                throw new ConflictException("Mã voucher đã tồn tại.");
            }

            voucher.Update(
                request.Code,
                request.DiscountPercent,
                request.MaxDiscountAmount,
                request.StartDate,
                request.EndDate,
                request.Quantity,
                request.Status
            );

            await voucherRepository.Update(voucher);

            return Unit.Value;
        }
    }
}