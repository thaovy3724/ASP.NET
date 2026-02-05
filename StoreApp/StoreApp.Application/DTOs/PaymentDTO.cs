using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.DTOs
{
    public sealed record PaymentDTO(Guid Id, Guid OrderId, DateTime PaymentDate, decimal Amount, Core.ValueObject.PaymentMethod PaymentMethod);
}
