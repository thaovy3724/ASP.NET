using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Barcode:    entity.Barcode,
                Price:      entity.Price,
                Unit:       entity.Unit,
                ImageUrl:   entity.ImageUrl,
                CreatedAt: entity.CreatedAt
            );
        }
    }
}
