using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Remove
{
    public class RemoveSupplierHandler(ISupplierRepository supplierRepository) : IRequestHandler<RemoveSupplierCommand, Result>
    {
        public async Task<Result> Handle(RemoveSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetById(request.Id);
            if (supplier == null)
            {
                return new Result(
                    Success: false,
                    Message: "Nhà cung cấp không tồn tại."
                );
            }
            if(await supplierRepository.IsExistProductOfSupplier(request.Id))
            {
                return new Result(
                    Success: false,
                    Message: "Không thể xóa nhà cung cấp vì còn sản phẩm liên quan."
                );
            }
            await supplierRepository.Delete(supplier);
            return new Result(
                Success: true,
                Message: "Xóa nhà cung cấp thành công."
            );
        }
    }
}
