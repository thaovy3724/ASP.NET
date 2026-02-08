using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.DTOs
{
    public sealed record PromotionDTO(Guid Id, string Code, string Description, decimal DiscountValue, DateTime StartDate, DateTime EndDate);
}
