using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Application.UseCases.InventoryUseCase.Query.GetList;

namespace StoreApp.Application.UseCases.GRNUseCase.Query.GetList
{
    public class GetListGRNHandler(IGRNRepository inventoryRepository) : IRequestHandler<GetListGRNQuery, ResultWithData<List<GRNDTO>>>
    {
        public async Task<ResultWithData<List<GRNDTO>>> Handle(GetListInventoryQuery request, CancellationToken cancellationToken)
        {
            // getAll() trong BaseRepository của tầng application
            var gr
            var list = await inventoryRepository.GetAll();
            var dto = list.Select(x => x.ToDTO()).ToList();     // entity => DTO

            return new ResultWithData<List<GRNDTO>>(
                Success: true,
                Message: "Lấy danh sách tồn kho thành công.",
                Data: dto
            );
        }

        public Task<ResultWithData<List<GRNDTO>>> Handle(GetListGRNQuery request, CancellationToken cancellationToken)
        {
            var grnList = new List<GRNDTO>();
            if(request.SupplierId )
        }
    }
}
