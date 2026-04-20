using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Delete
{
    public class DeleteProductHandler(
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IGRNRepository grnRepository
    ) : IRequestHandler<DeleteProductCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra product có tồn tại không
            var product = await productRepository.GetById(request.Id);
            if (product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại.");
            }

            // Kiểm tra product có nằm trong order nào không
             var existsInOrder = await orderRepository.HasProductReference(request.Id);
             if (existsInOrder)
             {
                throw new ConflictException("Sản phẩm đang nằm trong đơn hàng, không thể xóa.");
             }

            if (await grnRepository.HasProductReference(product.Id))
            {
                throw new ConflictException($"Sản phẩm đang nằm trong phiếu nhập, không thể xóa.");
            }


            // Kiểm tra tồn kho trước khi xóa sản phẩm, nếu tồn kho khác 0 thì không cho xóa sản phẩm
            product.EnsureCanBeDeleted();

            // Xóa product
            await productRepository.Delete(product);

            return Unit.Value;
        }
    }
}