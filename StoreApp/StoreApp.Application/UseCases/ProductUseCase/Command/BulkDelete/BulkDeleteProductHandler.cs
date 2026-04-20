using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.BulkDelete
{
    public class BulkDeleteProductHandler(
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IGRNRepository grnRepository
    ) : IRequestHandler<BulkDeleteProductCommand, Unit>
    {
        public async Task<Unit> Handle(BulkDeleteProductCommand request, CancellationToken cancellationToken)
        {
            var ids = request.Ids?
                .Where(id => id != Guid.Empty)
                .Distinct()
                .ToList() ?? [];

            if (ids.Count == 0)
            {
                throw new BadRequestException("Phải chọn ít nhất một sản phẩm để xóa.");
            }

            var products = await productRepository.GetByIds(ids);

            var foundIds = products.Select(x => x.Id).ToHashSet();
            var missingIds = ids.Where(id => !foundIds.Contains(id)).ToList();

            if (missingIds.Count > 0)
            {
                throw new NotFoundException("Có sản phẩm không tồn tại.");
            }

            foreach (var product in products)
            {
                if (await orderRepository.HasProductReference(product.Id))
                {
                    throw new ConflictException($"Sản phẩm \"{product.ProductName}\" đang nằm trong đơn hàng, không thể xóa.");
                }

                if (await grnRepository.HasProductReference(product.Id))
                {
                    throw new ConflictException($"Sản phẩm \"{product.ProductName}\" đang nằm trong phiếu nhập, không thể xóa.");
                }

                product.EnsureCanBeDeleted();
            }

            await productRepository.DeleteRange(products);

            return Unit.Value;
        }
    }
}