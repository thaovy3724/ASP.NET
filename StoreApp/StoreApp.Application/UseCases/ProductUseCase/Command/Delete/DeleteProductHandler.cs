using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Delete
{
    public class DeleteProductHandler(
        IProductRepository productRepository,
        IInventoryRepository inventoryRepository
    // IOrderRepository orderRepository // dùng khi có Order
    ) : IRequestHandler<DeleteProductCommand, Result>
    {
        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Kiểm tra product có tồn tại không
            var product = await productRepository.GetById(request.Id);
            if (product is null)
            {
                return new Result(
                    Success: false,
                    Message: "Sản phẩm không tồn tại."
                );
            }

            // kiểm tra product có nằm trong order nào không
            // var existsInOrder = await orderRepository.ExistsItemWithProductId(request.Id);
            // if (existsInOrder)
            // {
            //     return new Result(
            //         Success: false,
            //         Message: "Không thể xóa sản phẩm đang nằm trong đơn hàng."
            //     );
            // }

            // Lấy inventory theo product
            var inventory = await inventoryRepository.GetByProductID(request.Id);
            if (inventory is not null && inventory.Quantity != 0)
            {
                return new Result(
                    Success: false,
                    Message: "Không thể xóa sản phẩm khi tồn kho khác 0."
                );
            }

            // 4Xóa inventory trước (nếu có)
            if (inventory is not null)
            {
                await inventoryRepository.Delete(inventory);
            }

            // Xóa product
            await productRepository.Delete(product);

            return new Result(
                Success: true,
                Message: "Xóa sản phẩm thành công."
            );
        }
    }
}
