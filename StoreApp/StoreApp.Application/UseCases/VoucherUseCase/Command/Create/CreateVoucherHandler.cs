using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.VoucherUseCase.Command.Create
{
    public class CreateVoucherHandler(IVoucherRepository voucherRepository)
        : IRequestHandler<CreateVoucherCommand, VoucherDTO>
    {
        public async Task<VoucherDTO> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
        {
            if (await voucherRepository.IsCodeExist(request.Code))
            {
                throw new ConflictException("Mã voucher đã tồn tại.");
            }

            var voucher = new Voucher(
                request.Code,
                request.DiscountPercent,
                request.MaxDiscountAmount,
                request.StartDate,
                request.EndDate,
                request.Quantity,
                request.Status
            );

            await voucherRepository.Create(voucher);

            return voucher.ToDTO();
        }
    }
}