using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.ProductReviewUseCase.Query.GetByProduct
{
    public class GetProductReviewsHandler(
        IProductRepository productRepository,
        IProductReviewRepository productReviewRepository)
        : IRequestHandler<GetProductReviewsQuery, List<ProductReviewDTO>>
    {
        public async Task<List<ProductReviewDTO>> Handle(GetProductReviewsQuery request, CancellationToken cancellationToken)
        {
            if (!await productRepository.IsExist(x => x.Id == request.ProductId))
            {
                throw new NotFoundException("Sản phẩm không tồn tại.");
            }

            var reviews = await productReviewRepository.GetByProductId(request.ProductId);

            return reviews.Select(x => x.ToDTO()).ToList();
        }
    }
}