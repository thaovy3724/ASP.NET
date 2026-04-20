using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetDeletedList
{
    public class GetDeletedProductHandler(IProductRepository productRepository) : IRequestHandler<GetDeletedProductQuery, PagedList<ProductDTO>>
    {
        public async Task<PagedList<ProductDTO>> Handle(GetDeletedProductQuery request, CancellationToken cancellationToken)
        {
            var result = await productRepository.Search(
                request.PageNumber,
                request.PageSize,
                request.CategoryId,
                request.SupplierId,
                request.MinPrice,
                request.MaxPrice,
                request.Keyword,
                isDeleted: true);

            var productListDTO = result.Items
                .Select(product => product.ToDTO())
                .ToList();

            return new PagedList<ProductDTO>(productListDTO, result.MetaData);
        }
    }
}