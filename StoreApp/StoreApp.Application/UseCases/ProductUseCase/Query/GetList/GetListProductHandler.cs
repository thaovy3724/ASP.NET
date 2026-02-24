using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetList
{
    public class GetListProductHandler(IProductRepository productRepository) : IRequestHandler<GetListProductQuery, ResultWithData<List<ProductDTO>>>
    {
        public async Task<ResultWithData<List<ProductDTO>>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.Search(request.CategoryId, request.MinPrice, request.MaxPrice, request.Keyword);

            var productListDTO = products
                .Select(product => product.ToDTO())     // entity => DTO 
                .ToList();

            // Trả về kết quả
            return new ResultWithData<List<ProductDTO>>(
                Success: true,
                Message: "Lấy danh sách sản phẩm  thành công.",
                Data: productListDTO
            );
        }
    }
}
