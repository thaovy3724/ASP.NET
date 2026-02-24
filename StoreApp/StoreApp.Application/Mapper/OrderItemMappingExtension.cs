using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Mapper
{
    public static class OrderItemMappingExtension
    {
        public static OrderItemDTO ToDTO(this OrderDetail entity)
        {
            return new OrderItemDTO
            (
                ProductId: entity.ProductId,
                Quantity: entity.Quantity,
                UnitPrice: entity.Price,
                TotalPrice: entity.Subtotal
            );
        }
    }
}
