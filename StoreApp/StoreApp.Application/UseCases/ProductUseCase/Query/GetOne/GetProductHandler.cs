using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;


namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetOne
{
    public class GetProductHandler(IProductRepository productRepository) : IRequestHandler<GetProductQuery, ProductDTO>
    {
        public async Task<ProductDTO> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            // gọi repository ở tầng Application 
            var product = await productRepository.GetById(request.Id);
            if(product is null || product.IsDeleted)
            {
                throw new NotFoundException("Sản phẩm không tồn tại.");
            }
            return product.ToDTO();
        }
    }
}
