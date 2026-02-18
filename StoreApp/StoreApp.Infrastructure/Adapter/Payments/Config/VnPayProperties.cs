using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructure.Adapters.Payment.Config
{
    public class VnPayProperties
    {
        public string TmnCode { get; set; } = string.Empty;
        public string HashSecret { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string Command { get; set; } = "pay";
        public string CurrCode { get; set; } = "VND";
        public string Version { get; set; } = "2.1.0";
        public string Locale { get; set; } = "vn";
        public string ReturnUrl { get; set; } = string.Empty;

        // Thêm trường này nếu trong appsettings bạn đặt tên key là "PayUrl" 
        // (để chứa link https://sandbox.vnpayment.vn/paymentv2/vpcpay.html)
        public string PaymentUrl { get; set; } = string.Empty;
        public int PaymentTimeout { get; set; } = 15; // Thời gian timeout mặc định là 15 phút
    }
}