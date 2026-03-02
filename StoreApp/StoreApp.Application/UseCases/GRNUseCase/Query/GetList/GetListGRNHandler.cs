using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.GRNUseCase.Query.GetList
{
    public class GetListGRNHandler(IGRNRepository grnRepository) : IRequestHandler<GetListGRNQuery, List<GRNDTO>>
    {
        public async Task<List<GRNDTO>> Handle(GetListGRNQuery request, CancellationToken cancellationToken)
        {
            var grns = await grnRepository.Search(request.Supplier, request.GRNStatus);
            var grnListDTO = grns.Select(grn => grn.ToDTO()).ToList();

            return grnListDTO;
        }
    }
}
