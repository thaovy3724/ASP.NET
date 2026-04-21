using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.VoucherUseCase.Query.GetAvailable
{
    public class GetAvailableVoucherHandler(IVoucherRepository voucherRepository)
        : IRequestHandler<GetAvailableVoucherQuery, List<AvailableVoucherDTO>>
    {
        public async Task<List<AvailableVoucherDTO>> Handle(
            GetAvailableVoucherQuery request,
            CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow.AddHours(7);

            var vouchers = await voucherRepository.GetAvailableVouchers(now);

            return vouchers.Select(x => new AvailableVoucherDTO(
                Code: x.Code,
                DiscountPercent: x.DiscountPercent,
                MaxDiscountAmount: x.MaxDiscountAmount,
                EndDate: x.EndDate
            )).ToList();
        }
    }
}