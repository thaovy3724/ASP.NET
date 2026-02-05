using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Mapper
{
    public static class PromotionMappingExtension
    {
        // Add mapping methods for Promotion entity and DTO here
        public static PromotionDTO ToDTO(this Promotion promotion)
        {
            return new PromotionDTO
            (
                Id: promotion.Id,
                Code: promotion.PromoCode,
                Description: promotion.Description,
                DiscountValue: promotion.DiscountValue,
                StartDate: promotion.StartDate,
                EndDate: promotion.EndDate
            );
        }
    }
}
