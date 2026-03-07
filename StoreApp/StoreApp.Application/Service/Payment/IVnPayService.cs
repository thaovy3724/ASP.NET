using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Service.Payment
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(Guid id, decimal totalAmount);
        PaymentResponseModel PaymentExecute(Dictionary<string, string> collections);
    }
}
