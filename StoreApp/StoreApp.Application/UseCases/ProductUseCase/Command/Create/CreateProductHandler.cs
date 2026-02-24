using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Create
{
    public class CreateProductHandler(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository,     // khóa ngoại 
        ISupplierRepository supplierRepository)   // sửa inventory khi update 
        : IRequestHandler<CreateProductCommand, ResultWithData<ProductDTO>>
    {
        public async Task<ResultWithData<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // kiểm tra CategoryId tồn tại chưa
            var category = await categoryRepository.GetById(request.CategoryId);
            if (category is null) 
            { 
                throw new NotFoundException("Thể loại không tồn tại");
            } 

            // kiểm tra SupplierId tồn tại chưa
            var supplier = await supplierRepository.GetById(request.SupplierId);
            if (supplier is null)
            {
                throw new NotFoundException("Nhà cung cấp không tồn tại");
            }

            // truyền data vào Product Entity
            var product = new Product(
                request.CategoryId,
                request.SupplierId,
                request.ProductName,
                request.Price,
                request.ImageUrl
            );

            // cuối cùng gọi hàm Create trong IProductRepository (tầng Application) để thêm product vào db 
            await productRepository.Create(product);

            return new ResultWithData<ProductDTO>(
                Success: true,
                Message: "Tạo sản phẩm thành công",
                Data: product.ToDTO()
                );
        }
    }
}
