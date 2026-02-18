using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Update
{
    public class UpdateProductHandler(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository, 
        ISupplierRepository supplierRepository) 
        : IRequestHandler<UpdateProductCommand, Result>
    {
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            // kiểm tra sản phẩm đang update có trong db không
            var product = await productRepository.GetById(request.ProductId);
            if (product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại");  
            }

            // kiểm tra CategoryId tồn tại không
            var category = await categoryRepository.GetById(request.CategoryId);
            if (category is null) 
            { 
                return new Result(
                    false, 
                    "CategoryId không tồn tại"
                );
            }
            // kiểm tra SupplierId tồn tại không
            var supplier = await supplierRepository.GetById(request.SupplierId);
            if (supplier is null)
            {
                return new Result(
                    false,
                    "SupplierId không tồn tại"
                );
            }

            // kiểm tra barcode vừa nhập có trùng với product nào khác không 
            if (await productRepository.ExistsBarcodeForOtherProducts(request.ProductId, request.Barcode))
            { 
                return new Result(
                    Success: false,
                    Message: "Barcode đã tồn tại ở sản phẩm khác"
                    );
            }

            // pass hết thì gọi Update để truyền data vào Product Entity 
            product.Update(
                request.CategoryId,
                request.SupplierId,
                request.ProductName,
                request.Barcode,
                request.Price,
                request.Unit,
                request.ImageUrl
            );

            // cuối cùng gọi hàm Update trong IProductRepository (tầng Application) để thực hiện update vào db 
            await productRepository.Update(product);

            return new Result(
                Success: true,
                Message: "Cập nhật sản phẩm thành công"
            );
        }
    }
}
