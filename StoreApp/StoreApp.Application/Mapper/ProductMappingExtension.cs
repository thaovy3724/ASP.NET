using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Mapper
{
    public static class ProductMappingExtension
    {
        public static ProductDTO ToDTO(this Product entity)
        {
            return new ProductDTO
            (
                Id:         entity.Id,
                CategoryId: entity.CategoryId,
                SupplierId: entity.SupplierId,
                ProductName:entity.ProductName,
                Price:      entity.Price,
                ImageUrl:   entity.ImageUrl
            );
        }
    }
}
