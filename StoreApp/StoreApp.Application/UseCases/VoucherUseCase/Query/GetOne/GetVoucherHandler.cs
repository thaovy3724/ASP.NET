using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.VoucherUseCase.Query.GetOne
{
    public class GetVoucherHandler(IVoucherRepository voucherRepository)
        : IRequestHandler<GetVoucherQuery, VoucherDTO>
    {
        public async Task<VoucherDTO> Handle(GetVoucherQuery request, CancellationToken cancellationToken)
        {
            var voucher = await voucherRepository.GetById(request.Id);
            if (voucher is null)
            {
                throw new NotFoundException("Voucher không tồn tại.");
            }

            return voucher.ToDTO();
        }
    }
}