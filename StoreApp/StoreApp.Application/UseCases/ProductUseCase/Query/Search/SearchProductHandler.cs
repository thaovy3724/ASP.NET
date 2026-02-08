using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.Search
{
    public class SearchProductHandler(IProductRepository productRepository) : IRequestHandler<SearchProductQuery, ResultWithData<List<ProductDTO>>>
    {
        public async Task<ResultWithData<List<ProductDTO>>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
        {
            var productList = await productRepository.Search(
                request.SupplierId,
                request.CategoryId,
                request.MinPrice,
                request.MaxPrice,
                request.Keyword,
                request.PriceOrder
            );

            var productDTO = productList.Select(x => x.ToDTO()).ToList();

            return new ResultWithData<List<ProductDTO>>(
                Success: true,
                Message: "Tìm kiếm danh sách sản phẩm thành công",
                Data: productDTO
            );
        }
    }
}
