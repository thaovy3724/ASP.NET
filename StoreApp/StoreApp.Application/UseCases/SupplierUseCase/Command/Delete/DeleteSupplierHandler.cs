using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Delete
{
    public class DeleteSupplierHandler(ISupplierRepository supplierRepository, IProductRepository productRepository) : IRequestHandler<DeleteSupplierCommand, Result>
    {
        public async Task<Result> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetById(request.Id);
            if (supplier is null)
            {
                throw new NotFoundException("Nhà cung cấp không tồn tại.");
            }

            if (await productRepository.IsExist(p => p.SupplierId == request.Id))
            {
                throw new ConflictException("Nhà cung cấp đang có sản phẩm liên kết, không thể xóa.");
            }

            await supplierRepository.Delete(supplier);
            return new Result(
                Success: true,
                Message: "Xóa nhà cung cấp thành công."
            );
        }
    }
}
