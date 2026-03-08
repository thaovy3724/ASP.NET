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
                Items: entity.Items
                    .Select(x => new GRNDetailDTO(x.ProductId, x.Quantity, x.Price))
                    .ToList()
            );
        }
    }
}
