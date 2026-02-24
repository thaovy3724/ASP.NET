using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Create
{
    public class CreateGRNHandler(IGRNRepository grnRepository, 
                                ISupplierRepository supplierRepository,
                                IProductRepository productRepository) : IRequestHandler<CreateGRNCommand, Result>
    {
        public async Task<Result> Handle(CreateGRNCommand request, CancellationToken cancellationToken)
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
            var grn = new GRN
            {
                SupplierId = request.SupplierId,
                GRNDate = DateTime.Now,
                Items = request.Items.Select(item => new GRNDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }
    }
}
