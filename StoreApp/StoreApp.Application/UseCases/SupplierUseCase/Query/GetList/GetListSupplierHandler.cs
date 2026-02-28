using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetList
{
    public class GetListSupplierHandler(ISupplierRepository supplierRepository) : IRequestHandler<GetListSupplierQuery, List<SupplierDTO>>
    {
        public async Task<List<SupplierDTO>> Handle(GetListSupplierQuery request, CancellationToken cancellationToken)
        {
            var suppliers = new List<Supplier>();
            if(string.IsNullOrEmpty(request.Keyword))
            {
                // Nếu không có từ khóa, lấy tất cả nhà cung cấp
                suppliers = await supplierRepository.GetAll();
            }
            else
            {
                suppliers = await supplierRepository.Search(request.Keyword);
            }

            var supplierDTO = suppliers
                .Select(supplier => supplier.ToDTO())
                .ToList();

            // Trả về kết quả trực tiếp
            return supplierDTO;
        }
    }
}
