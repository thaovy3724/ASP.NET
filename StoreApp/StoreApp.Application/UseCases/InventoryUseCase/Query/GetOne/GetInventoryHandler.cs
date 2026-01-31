using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Query.GetOne
{
    public class GetInventoryHandler(IInventoryRepository inventoryRepository) : IRequestHandler<GetInventoryQuery, ResultWithData<InventoryDTO>>
    {
        public async Task<ResultWithData<InventoryDTO>> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
        {
            var inv = await inventoryRepository.GetById(request.Id);
            if (inv is null)
            {
                return new ResultWithData<InventoryDTO>(
                    Success: false,
                    Message: "Không tìm thấy tồn kho",
                    Data: null);
            }

            return new ResultWithData<InventoryDTO>(
                Success: true,
                Message: "Lấy thông tin tồn kho thành công.",
                Data: inv.ToDTO()
            );
        }
    }
}
