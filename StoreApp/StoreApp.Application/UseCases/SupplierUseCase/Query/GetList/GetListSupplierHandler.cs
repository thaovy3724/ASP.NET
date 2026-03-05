using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.SupplierUseCase.Query.GetList
{
    public class GetListSupplierHandler(ISupplierRepository supplierRepository) : IRequestHandler<GetListSupplierQuery, PagedList<SupplierDTO>>
    {
        public async Task<PagedList<SupplierDTO>> Handle(GetListSupplierQuery request, CancellationToken cancellationToken)
        {
            var result = await supplierRepository.Search(request.PageNumber, request.PageSize, request.Keyword);
        
            var supplierListDTO = result.Items
                .Select(supplier => supplier.ToDTO())
                .ToList();

            // Trả về kết quả trực tiếp
            return new PagedList<SupplierDTO>(supplierListDTO, result.MetaData);
        }
    }
}
