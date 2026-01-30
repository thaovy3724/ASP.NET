using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.ProductUseCase.Command.Create
{
    public class CreateProductHandler(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository,     // khóa ngoại 
        ISupplierRepository supplierRepository,     // khóa ngoại 
        IInventoryRepository inventoryRepository)   // sửa inventory khi update 
        : IRequestHandler<CreateProductCommand, ResultWithData<ProductDTO>>
    {
        public async Task<ResultWithData<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // kiểm tra CategoryId tồn tại chưa
            var category = await categoryRepository.GetById(request.CategoryId);
            if (category is null) 
            { 
                return new ResultWithData<ProductDTO>(
                    false,
                    "CategoryId không tồn tại", 
                    null
                );
            }

            // kiểm tra SupplierId tồn tại chưa
            var supplier = await supplierRepository.GetById(request.SupplierId);
            if (supplier is null)
            { 
                return new ResultWithData<ProductDTO>(
                    false, 
                    "SupplierId không tồn tại", 
                    null
                );
            }

            // kiểm tra barcord này tồn tại chưa 
            if (await productRepository.ExistsBarcode(request.Barcode))
            {
                return new ResultWithData<ProductDTO>(
                    Success: false, 
                    Message: "Barcode đã tồn tại", null
                );
            }

            // truyền data vào Product Entity
            var product = new Product(
                request.CategoryId,
                request.SupplierId,
                request.ProductName,
                request.Barcode,
                request.Price,
                request.Unit,
                request.ImageUrl
            );

            // cuối cùng gọi hàm Create trong IProductRepository (tầng Application) để thêm product vào db 
            await productRepository.Create(product);

            // Auto-create inventory cho product vừa tạo (mặc định quantity = 0)
            var existedInventory = await inventoryRepository.GetByProductID(product.Id);
            if (existedInventory is null)   // check tránh tạo trùng (do unique index product_id)
            {
                var inventory = new Inventory(product.Id, 0);
                await inventoryRepository.Create(inventory);
            }

            return new ResultWithData<ProductDTO>(
                Success: true,
                Message: "Tạo sản phẩm thành công",
                Data: product.ToDTO()
                );
        }
    }
}
