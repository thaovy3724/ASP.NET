using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.DTOs
{
    public class PaymentResponseModel
    {
        // Trạng thái giao dịch (True = Thành công, False = Thất bại)
        public bool Success { get; set; }

        // Mã đơn hàng nội bộ của bạn (Guid hoặc Int)
        public Guid OrderId { get; set; }

        // Mã giao dịch (Dùng để tra soát)
        public string PaymentId { get; set; } = string.Empty;

        // Mã giao dịch từ phía cổng thanh toán (vnp_TransactionNo)
        public string TransactionId { get; set; } = string.Empty;

        // Mã lỗi chi tiết từ VNPay (VD: "00" là thành công, "24" là hủy)
        // Lưu lại để sau này debug nếu khách kêu lỗi
        public string VnPayResponseCode { get; set; } = string.Empty;
    }
}