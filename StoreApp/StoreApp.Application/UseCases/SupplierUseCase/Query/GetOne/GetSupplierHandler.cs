using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetOne
{
    public class GetSupplierHandler(ISupplierRepository supplierRepository) : IRequestHandler<GetSupplierQuery, SupplierDTO>
    {
        public async Task<SupplierDTO> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetById(request.Id);
            if(supplier == null)
            {
                throw new NotFoundException("Nhà cung cấp không tồn tại.");
            }
            // Trả về kết quả trực tiếp
            return supplier.ToDTO();
        }
    }
}
