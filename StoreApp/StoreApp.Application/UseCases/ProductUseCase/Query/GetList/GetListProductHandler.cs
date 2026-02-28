using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetList
{
    public class GetListProductHandler(IProductRepository productRepository) : IRequestHandler<GetListProductQuery, List<ProductDTO>>
    {
        public async Task<List<ProductDTO>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.Search(request.CategoryId, request.MinPrice, request.MaxPrice, request.Keyword);

            var productListDTO = products
                .Select(product => product.ToDTO())     // entity => DTO 
                .ToList();

            // Trả về kết quả
            return productListDTO;
        }
    }
}
