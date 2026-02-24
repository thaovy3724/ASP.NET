using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Command.Update
{
    public class UpdateInventoryHandler(IGRNRepository inventoryRepository) : IRequestHandler<UpdateInventoryCommand, Result>
    {
        public async Task<Result> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {

            // kiểm tra Inventory có tồn tại không 
            var inv = await inventoryRepository.GetById(request.InventoryId);
            if (inv is null)
            {
               throw new NotFoundException($"Không tìm thấy tồn kho với Id: {request.InventoryId}");
            }

            inv.UpdateQuantity(request.Quantity);   // trong entity có set UpdatedAt 
            await inventoryRepository.Update(inv);

            return new Result(true, "Cập nhật tồn kho thành công");
        }
    }
}
