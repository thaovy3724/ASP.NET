using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.GRNUseCase.Query.GetList
{
    public class GetListGRNHandler(IGRNRepository grnRepository) : IRequestHandler<GetListGRNQuery, PagedList<GRNDTO>>
    {
        public async Task<PagedList<GRNDTO>> Handle(GetListGRNQuery request, CancellationToken cancellationToken)
        {
            var result = await grnRepository.Search(request.PageNumber, request.PageSize, request.Supplier, request.GRNStatus);
            var grnListDTO = result.Items
                .Select(grn => grn.ToDTO())
                .ToList();

            return new PagedList<GRNDTO>(grnListDTO, result.MetaData);
        }
    }
}
