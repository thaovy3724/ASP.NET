using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.ProductReviewUseCase.Command.Create
{
    public class CreateProductReviewHandler(
        IProductRepository productRepository,
        IProductReviewRepository productReviewRepository)
        : IRequestHandler<CreateProductReviewCommand, ProductReviewDTO>
    {
        public async Task<ProductReviewDTO> Handle(CreateProductReviewCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(request.ProductId);

            if (product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại.");
            }

            var customerId = request.CustomerId!.Value;

            var hasPurchased = await productReviewRepository.HasCustomerPurchasedProduct(customerId, request.ProductId);

            if (!hasPurchased)
            {
                throw new ConflictException("Bạn chỉ được đánh giá sản phẩm sau khi đã mua sản phẩm này.");
            }

            var hasReviewed = await productReviewRepository.HasCustomerReviewed(customerId, request.ProductId);

            if (hasReviewed)
            {
                throw new ConflictException("Bạn đã đánh giá sản phẩm này rồi.");
            }

            var review = new ProductReview(
                request.ProductId,
                customerId,
                request.Rating,
                string.IsNullOrWhiteSpace(request.Comment) ? null : request.Comment.Trim()
            );

            await productReviewRepository.Create(review);

            return review.ToDTO();
        }
    }
}