using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Delete
{
    public class DeleteListOfProductHandler(IProductRepository productRepository) : IRequestHandler<DeleteListOfProductCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteListOfProductCommand request, CancellationToken cancellationToken)
        {
            List<Guid> productIds = request.ProductIds;
            if(productIds == null || productIds.Count == 0)
            {
                return Unit.Value; // Không có sản phẩm nào để xóa, trả về thành công
            } else
            {
                foreach(Guid id in productIds)
                {
                    var product = await productRepository.GetById(id);
                    if (product is null)
                    {
                        throw new NotFoundException($"Sản phẩm với id {id} không tồn tại.");
                    }
                    // Kiểm tra tồn kho trước khi xóa sản phẩm, nếu tồn kho khác 0 thì không cho xóa sản phẩm
                    product.EnsureCanBeDeleted();
                }
            }
            await productRepository.DeleteListOfProduct(productIds);
            return Unit.Value;
        }
    }
}
