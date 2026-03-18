using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.DTOs
{
    public class VnPayCallbackRequest
    {
        [FromQuery(Name = "vnp_Amount")] public long Amount { get; set; }
        [FromQuery(Name = "vnp_ResponseCode")] public string ResponseCode { get; set; }
        [FromQuery(Name = "vnp_TransactionStatus")] public string TransactionStatus { get; set; }
        [FromQuery(Name = "vnp_TxnRef")] public string OrderId { get; set; }
        [FromQuery(Name = "vnp_SecureHash")] public string SecureHash { get; set; }
    }
}