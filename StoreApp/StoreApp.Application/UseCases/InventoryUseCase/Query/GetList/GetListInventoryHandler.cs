using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Query.GetList
{
    public class GetListInventoryHandler(IInventoryRepository inventoryRepository) : IRequestHandler<GetListInventoryQuery, ResultWithData<List<InventoryDTO>>>
    {
        public async Task<ResultWithData<List<InventoryDTO>>> Handle(GetListInventoryQuery request, CancellationToken cancellationToken)
        {
            // getAll() trong BaseRepository của tầng application
            var list = await inventoryRepository.GetAll();
            var dto = list.Select(x => x.ToDTO()).ToList();     // entity => DTO

            return new ResultWithData<List<InventoryDTO>>(
                Success: true,
                Message: "Lấy danh sách tồn kho thành công.",
                Data: dto
            );
        }
    }
}
