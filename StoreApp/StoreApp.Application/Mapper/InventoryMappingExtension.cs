using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Mapper
{
    public static class InventoryMappingExtension
    {
        public static GRNDTO ToDTO(this GRN entity)
        {
            return new InventoryDTO(
                Id: entity.Id,
                ProductId: entity.ProductId,    
                Quantity: entity.Quantity,
                UpdatedAt: entity.UpdatedAt
            );
        }
    }
}
