using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Application.UseCases.SupplierUseCase.Query.GetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetOne
{
    public class GetSupplierHandler(ISupplierRepository supplierRepository) : IRequestHandler<GetSupplierQuery, ResultWithData<SupplierDTO>>
    {
        public async Task<ResultWithData<SupplierDTO>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetById(request.Id);
            if(supplier == null)
            {
                throw new NotFoundException("Nhà cung cấp không tồn tại.");
            }
            // Trả về kết quả trực tiếp
            var dto = supplier.ToDTO();
            return new ResultWithData<SupplierDTO>(
                Success: true,
                Message: "Lấy nhà cung cấp thành công.",
                Data: dto
            );
        }
    }
}
