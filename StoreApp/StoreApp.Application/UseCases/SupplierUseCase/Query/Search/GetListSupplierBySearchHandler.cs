using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.Search
{
    public class GetListSupplierBySearchHandler(ISupplierRepository supplierRepository) : IRequestHandler<GetListSupplierBySearchQuery, ResultWithData<List<SupplierDTO>>>
    {
        public async Task<ResultWithData<List<SupplierDTO>>> Handle(GetListSupplierBySearchQuery request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(request.keyword))
            {
                return new ResultWithData<List<SupplierDTO>>(
                    Success: false,
                    Message: "Từ khóa tìm kiếm không được để trống.",
                    Data: []
                );
            }
            var suppliers = await supplierRepository.SearchByKeyword(request.keyword);
            var supplierDTO = suppliers
                .Select(supplier => supplier.ToDTO())
                .ToList();
            // Trả về kết quả trực tiếp
            return new ResultWithData<List<SupplierDTO>>(
                Success: true,
                Message: "Tìm kiếm danh sách nhà cung cấp thành công.",
                Data: supplierDTO
            );
        }
    }
}
