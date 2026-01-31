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
                return new Result(
                    Success: false,
                    Message: "Nhà cung cấp không tồn tại."
                );
            }
            if(await supplierRepository.IsSupplierNameExist(request.Name, request.Id))
            {
                return new Result(
                    Success: false,
                    Message: "Tên nhà cung cấp đã tồn tại."
                );
            }
            if(await supplierRepository.IsSupplierPhoneExist(request.Phone, request.Id))
            {
                return new Result(
                    Success: false,
                    Message: "Số điện thoại nhà cung cấp đã tồn tại."
                );
            }
            if(await supplierRepository.IsSupplierEmailExist(request.Email, request.Id))
            {
                return new Result(
                    Success: false,
                    Message: "Email nhà cung cấp đã tồn tại."
                );
            }
            //if(await supplierRepository.IsSupplierAddressExist(request.Address, request.Id))    
            //{
            //    return new Result(
            //        Success: false,
            //        Message: "Địa chỉ nhà cung cấp đã tồn tại."
            //    );
            //}
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
