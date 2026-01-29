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

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetList
{
    public class GetListSupplierHandler(ISupplierRepository supplierRepository) : IRequestHandler<GetListSupplierQuery, ResultWithData<List<SupplierDTO>>>
    {
        public async Task<ResultWithData<List<SupplierDTO>>> Handle(GetListSupplierQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await supplierRepository.GetAll();
            var supplierDTO = suppliers
                .Select(supplier => supplier.ToDTO())
                .ToList();
            // Trả về kết quả trực tiếp
            return new ResultWithData<List<SupplierDTO>>(
                Success: true,
                Message: "Lấy danh sách nhà cung cấp thành công.",
                Data: supplierDTO
            );
        }
    }
}
