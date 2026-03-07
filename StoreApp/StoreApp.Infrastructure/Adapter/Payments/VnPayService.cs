using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SM.Infrastructure.Adapters.Payment.Libs;
using StoreApp.Application.DTOs;
using StoreApp.Application.Service.Payment;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Exceptions;
public class VnPayService : IVnPayService
{
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public VnPayService(IConfiguration config, IHttpContextAccessor httpContextAccessor)
    {
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    public string CreatePaymentUrl(Guid id, decimal totalAmount)
    {
        var context = _httpContextAccessor.HttpContext;
        var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
        var pay = new VnPayLibrary();

        pay.AddRequestData("vnp_Version", "2.1.0");
        pay.AddRequestData("vnp_Command", "pay");
        pay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
        pay.AddRequestData("vnp_Amount", ((long)totalAmount * 100).ToString());
        pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
        pay.AddRequestData("vnp_ExpireDate", timeNow.AddMinutes(2).ToString("yyyyMMddHHmmss"));

        pay.AddRequestData("vnp_CurrCode", "VND");
        pay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
        pay.AddRequestData("vnp_Locale", "vn");
        pay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang {id}");
        pay.AddRequestData("vnp_OrderType", "other");
        pay.AddRequestData("vnp_ReturnUrl", _config["VnPay:ReturnUrl"]);
        pay.AddRequestData("vnp_TxnRef", id.ToString());

        var paymentUrl = pay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);
        return paymentUrl;
    }

    public PaymentResponseModel PaymentExecute(Dictionary<string, string> collections)
    {
        var pay = new VnPayLibrary();

        // 1. Duyệt và nạp dữ liệu
        foreach (var (key, value) in collections)
        {
            if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_") && key != "vnp_SecureHash" && key != "vnp_SecureHashType")
            {
                pay.AddResponseData(key, value.ToString());
            }
        }

        // 2. Lấy các tham số quan trọng
        var vnp_TxnRef = pay.GetResponseData("vnp_TxnRef");
        var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
        var vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode");

        // --- SỬA 1: IN LOG NGAY TẠI ĐÂY (Để chắc chắn nó chạy) ---
        Console.WriteLine($"------------[DEBUG VNPay] vnp_TxnRef: '{vnp_TxnRef}' | ResponseCode: '{vnp_ResponseCode}'");

        // --- SỬA 2: PARSE GUID TRƯỚC (Để dù sai chữ ký vẫn có ID trả về) ---
        Guid orderId = Guid.Empty;
        if (!Guid.TryParse(vnp_TxnRef, out orderId))
        {
            Console.WriteLine("---------------[DEBUG VNPay] Lỗi: Không thể ép kiểu sang Guid!");
            // Nếu không parse được thì return lỗi luôn (lúc này ID là Empty là đúng)
            throw new PaymentException("Lỗi: OrderId không hợp lệ");
        }

        // 3. Kiểm tra chữ ký (Bây giờ mới check)
        bool checkSignature = pay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
        if (!checkSignature)
        {
            Console.WriteLine("---------------[DEBUG VNPay] Lỗi: Sai chữ ký (Invalid Signature)!");

            throw new PaymentException("Lỗi: Sai chữ ký (Invalid Signature)");
        }

        // 4. Kiểm tra mã lỗi từ VNPay (Ví dụ: Khách hủy = 24)
        if (vnp_ResponseCode != "00")
        {
            Console.WriteLine($"---------------[DEBUG VNPay] Khách hủy hoặc lỗi. Code: {vnp_ResponseCode}");
            return new PaymentResponseModel
            {
                Success = false,
                VnPayResponseCode = vnp_ResponseCode,
                OrderId = orderId // Trả về ID để Controller xử lý
            };
        }

        // 5. Thành công
        return new PaymentResponseModel
        {
            Success = true,
            OrderId = orderId,
            PaymentId = vnp_SecureHash,
            TransactionId = pay.GetResponseData("vnp_TransactionNo"),
            VnPayResponseCode = vnp_ResponseCode
        };
    }
}