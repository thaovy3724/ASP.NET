using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Mapper
{
    public static class GRNMappingExtension
    {
        public static GRNDTO ToDTO(this GRN entity)
        {
            return new GRNDTO(
                Id: entity.Id,
                SupplierId: entity.SupplierId,
                GRNStatus: entity.Status,
                UpdatedAt: entity.UpdatedAt,
                Items: entity.Items.Select(item => new GRNDetailDTO(
                    ProductId: item.ProductId,
                    Quantity: item.Quantity,
                    Price: item.Price
                )).ToList()
            );
        }
    }
}
