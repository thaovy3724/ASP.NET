using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.VoucherUseCase.Query.GetList
{
    public class GetListVoucherHandler(IVoucherRepository voucherRepository)
        : IRequestHandler<GetListVoucherQuery, PagedList<VoucherDTO>>
    {
        public async Task<PagedList<VoucherDTO>> Handle(GetListVoucherQuery request, CancellationToken cancellationToken)
        {
            var result = await voucherRepository.Search(
                request.PageNumber,
                request.PageSize,
                request.Keyword
            );

            var dto = result.Items
                .Select(x => x.ToDTO())
                .ToList();

            return new PagedList<VoucherDTO>(dto, result.MetaData);
        }
    }
}