using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Mapper
{
    public static class PaymentMappingExtension
    {
        // Add mapping methods for Payment entity and DTO here
        public static PaymentDTO ToDTO(this Payment payment)
        {
            return new PaymentDTO
            (
                Id: payment.Id,
                OrderId: payment.OrderId,
                Amount: payment.Amount,
                PaymentDate: payment.PaymentDate,
                PaymentMethod: payment.PaymentMethod
            );
        }
    }
}
