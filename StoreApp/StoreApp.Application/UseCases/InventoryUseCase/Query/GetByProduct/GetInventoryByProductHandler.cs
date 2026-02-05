using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Application.UseCases.InventoryUseCase.Query.GetList;

namespace StoreApp.Application.UseCases.InventoryUseCase.Query.GetByProduct
{
    public class GetInventoryByProductHandler(IInventoryRepository inventoryRepository) : IRequestHandler<GetInventoryByProductQuery, ResultWithData<InventoryDTO>>
    {
        public async Task<ResultWithData<InventoryDTO>> Handle(GetInventoryByProductQuery request, CancellationToken cancellationToken)
        {
            var inventory = await inventoryRepository.GetByProductID(request.ProductId);

            if (inventory is null)
            {
                return new ResultWithData<InventoryDTO>(
                    Success: false,
                    Message: "Không tìm thấy tồn kho theo product",
                    Data: null
                );
            }

            return new ResultWithData<InventoryDTO>(
                Success: true,
                Message: "Lấy tồn kho theo product thành công",
                Data: inventory.ToDTO()
            );
        }
    }
}
