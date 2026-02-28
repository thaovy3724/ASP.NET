using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Complete
{
    public class CompleteGRNHandler(IGRNRepository grnRepository, IProductRepository productRepository) : IRequestHandler<CompleteGRNCommand, Unit>
    {
        public async Task<Unit> Handle(CompleteGRNCommand request, CancellationToken cancellationToken)
        {
            var grn = await grnRepository.GetById(request.Id);
            if(grn is null)
            {
                throw new NotFoundException($"Không tìm thấy phiếu nhập hàng với Id: {request.Id}");
            }

            grn.MarkAsCompleted();
            // Cập nhật tồn kho cho các sản phẩm trong phiếu nhập hàng
            foreach (var item in grn.Items)
            {
                var product = await productRepository.GetById(item.ProductId);
                if (product is null)
                {
                    throw new NotFoundException($"Không tìm thấy sản phẩm với Id: {item.ProductId}");
                }                     
            
                product.IncreaseStock(item.Quantity); // Tăng tồn kho
                await productRepository.Update(product);
            }
            return Unit.Value;
        }
    }
}
