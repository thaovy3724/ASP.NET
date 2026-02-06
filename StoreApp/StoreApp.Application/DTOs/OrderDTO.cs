using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.DTOs
{
    public sealed record OrderDTO(Guid Id, Guid? CustomerId, DateTime OrderDate, Guid UserId, Guid? PromoId, Core.ValueObject.OrderStatus OrderStatus, decimal DiscountAmount, decimal TotalAmount, IEnumerable<OrderItemDTO> Items);
}
