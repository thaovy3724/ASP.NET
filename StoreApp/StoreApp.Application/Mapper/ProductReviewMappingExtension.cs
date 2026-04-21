using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Mapper
{
    public static class ProductReviewMappingExtension
    {
        public static ProductReviewDTO ToDTO(this ProductReview entity)
        {
            return new ProductReviewDTO(
                Id: entity.Id,
                ProductId: entity.ProductId,
                CustomerId: entity.CustomerId,
                CustomerName: entity.Customer?.FullName ?? "Khách hàng",
                Rating: entity.Rating,
                Comment: entity.Comment,
                CreatedAt: entity.CreatedAt
            );
        }
    }
}