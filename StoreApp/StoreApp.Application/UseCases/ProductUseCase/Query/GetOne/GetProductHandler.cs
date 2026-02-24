using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;


namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetOne
{
    public class GetProductHandler(IProductRepository productRepository) : IRequestHandler<GetProductQuery, ResultWithData<ProductDTO>>
    {
        public async Task<ResultWithData<ProductDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            // gọi repository ở tầng Application 
            var product = await productRepository.GetById(request.Id);
            if(product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại.");
            }
            var productDTO = product.ToDTO();
            return new ResultWithData<ProductDTO>(
                Success: true,
                Message: "Lấy thông tin sản phẩm thành công.",
                Data: productDTO
                );
        }
    }
}
