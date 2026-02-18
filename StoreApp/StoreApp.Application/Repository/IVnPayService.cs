using StoreApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Repository
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(OrderDTO order);
        PaymentResponseModel PaymentExecute(Dictionary<string, string> collections);
    }
}
