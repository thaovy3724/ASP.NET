using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Update
{
    public class UpdateSupplierHandler(ISupplierRepository supplierRepository) : IRequestHandler<UpdateSupplierCommand, Result>
    {
        public async Task<Result> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplierInDb = await supplierRepository.GetById(request.Id);
            if (supplierInDb == null)
            {
                throw new NotFoundException("Nhà cung cấp không tồn tại.");
            }
            if(await supplierRepository.IsSupplierNameExist(request.Name, request.Id))
            {
                throw new ConflictException("Tên nhà cung cấp đã tồn tại.");
            }
            if(await supplierRepository.IsSupplierPhoneExist(request.Phone, request.Id))
            {
                throw new ConflictException("Số điện thoại nhà cung cấp đã tồn tại.");
            }
            if(await supplierRepository.IsSupplierEmailExist(request.Email, request.Id))
            {
                throw new ConflictException("Email nhà cung cấp đã tồn tại.");
            }
            var supplier = await supplierRepository.GetById(request.Id);
            supplier.Update(
                request.Name,
                request.Phone,
                request.Email,
                request.Address
            );
            await supplierRepository.Update(supplier);
            return new Result(
                Success: true,
                Message: "Cập nhật nhà cung cấp thành công."
            );
        }
    }
}
