using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.InventoryUseCase.Command.Delete
{
    public class DeleteInventoryHandler(IInventoryRepository inventoryRepository) : IRequestHandler<DeleteInventoryCommand, Result>
    {
        public async Task<Result> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            // kiểm tra inventory có trong db chưa 
            var inv = await inventoryRepository.GetById(request.Id);
            if (inv is null)
            {
                return new Result(false, "Không tìm thấy tồn kho");
            }

            await inventoryRepository.Delete(inv);
            return new Result(true, "Xoá tồn kho thành công");
        }
    }
}
