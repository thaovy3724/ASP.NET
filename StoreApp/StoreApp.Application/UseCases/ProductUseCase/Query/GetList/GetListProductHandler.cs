using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetList
{
    public class GetListProductHandler(IProductRepository productRepository) : IRequestHandler<GetListProductQuery, PagedList<ProductDTO>>
    {
        public async Task<PagedList<ProductDTO>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            var result = await productRepository
                          .Search(
                            request.PageNumber,
                            request.PageSize,
                            request.CategoryId,
                            request.SupplierId,
                            request.MinPrice, 
                            request.MaxPrice, 
                            request.Keyword);

            var productListDTO = result.Items
                .Select(product => product.ToDTO())     // entity => DTO 
                .ToList();

            // Trả về kết quả
            return new PagedList<ProductDTO>(productListDTO, result.MetaData);
        }
    }
}
