using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Update
{
    public class UpdateProductHandler(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository, 
        ISupplierRepository supplierRepository) 
        : IRequestHandler<UpdateProductCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            // kiểm tra sản phẩm đang update có trong db không
            var product = await productRepository.GetById(request.Id);
            if (product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại");  
            }

            // kiểm tra CategoryId tồn tại không
            var category = await categoryRepository.GetById(request.CategoryId);
            if (category is null) 
            { 
                throw new NotFoundException("Thể loại không tồn tại");
            }

            // pass hết thì gọi Update để truyền data vào Product Entity 
            product.Update(
                request.CategoryId,
                request.ProductName,
                request.Price,
                request.ImageUrl
            );

            // cuối cùng gọi hàm Update trong IProductRepository (tầng Application) để thực hiện update vào db 
            await productRepository.Update(product);

            return Unit.Value;
        }
    }
}
