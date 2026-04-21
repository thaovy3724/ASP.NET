using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.ProductUseCase.Query.GetOne
{
    public class GetProductHandler(
        IProductRepository productRepository,
        IProductReviewRepository productReviewRepository)
        : IRequestHandler<GetProductQuery, ProductDetailDTO>
    {
        public async Task<ProductDetailDTO> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(request.Id);

            if (product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại.");
            }

            var summary = await productReviewRepository.GetSummary(request.Id);

            return product.ToDetailDTO(summary.AverageRating, summary.ReviewCount);
        }
    }
}