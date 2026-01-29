using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Create
{
    public class CreateSupplierHandler(ISupplierRepository supplierRepository) : IRequestHandler<CreateSupplierCommand, ResultWithData<SupplierDTO>>
    {
        public async Task<ResultWithData<SupplierDTO>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            if(await supplierRepository.IsSupplierExist(request.Name, request.Email, request.Phone))
            {
                return new ResultWithData<SupplierDTO>(
                    Success: false,
                    Message: "Nhà cung cấp đã tồn tại.",
                    Data: null
                );
            }
            var entity = new Supplier(
                request.Name,
                request.Phone,
                request.Email,
                request.Address
            );
            await supplierRepository.Create(entity);
            return new ResultWithData<SupplierDTO>(
                Success: true,
                Message: "Tạo nhà cung cấp thành công.",
                Data: entity.ToDTO()
            );
        }
    }
}
