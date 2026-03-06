using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Service.Payment
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(Order order);
        PaymentResponseModel PaymentExecute(Dictionary<string, string> collections);
    }
}
