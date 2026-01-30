using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.InventoryUseCase.Command.Create
{
    public class CreateInventoryHandler(
        IInventoryRepository inventoryRepository,
        IProductRepository productRepository)
        : IRequestHandler<CreateInventoryCommand, ResultWithData<InventoryDTO>>
    {
        public async Task<ResultWithData<InventoryDTO>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {

            // check ProductId tồn tại
            var product = await productRepository.GetById(request.ProductId);
            if (product is null)
            {
                return new ResultWithData<InventoryDTO>(
                    Success: false,
                    Message: "ProductId không tồn tại",
                    Data: null);
            }

            // check đã có inventory cho product chưa
            var existed = await inventoryRepository.GetByProductID(request.ProductId);
            if (existed is not null)
            {
                return new ResultWithData<InventoryDTO>(
                    Success: false,
                    Message: "Sản phẩm đã có tồn kho",
                    Data: null);
            }

            var inv = new Inventory(request.ProductId, request.Quantity);
            await inventoryRepository.Create(inv);

            return new ResultWithData<InventoryDTO>(
                Success: true,
                Message: "Tạo tồn kho thành công",
                Data: inv.ToDTO());
        }
    }
}
