using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Create
{
    public class CreateGRNHandler(IGRNRepository grnRepository, 
                                ISupplierRepository supplierRepository,
                                IProductRepository productRepository) : IRequestHandler<CreateGRNCommand, GRNDTO>
    {
        public async Task<GRNDTO> Handle(CreateGRNCommand request, CancellationToken cancellationToken)
        {
            // Kiểm tra tồn tại nhà cung cấp (Supplier)
            var supplier = await supplierRepository.GetById(request.SupplierId);
            if(supplier is null)
            {
                throw new NotFoundException("Nhà cung cấp không tồn tại");
            }

            // Kiểm tra tồn tại sản phẩm (Product)
            request.Items.ForEach(item => 
            {
                var product = productRepository.GetById(item.ProductId);
                if(product is null)
                {
                    throw new NotFoundException($"Sản phẩm với ID {item.ProductId} không tồn tại");
                }
            });

            // Tạo mới GRN
            var grn = new GRN(request.SupplierId);

            // Thêm các item vào GRN
            request.Items.ForEach(item => grn.AddItem(item.ProductId, item.Quantity, item.Price));
            await grnRepository.Create(grn);
            return grn.ToDTO();
        }
    }
}
