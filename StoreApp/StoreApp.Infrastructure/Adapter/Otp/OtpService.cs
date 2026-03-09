using Microsoft.Extensions.Caching.Memory;
using StoreApp.Application.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Adapter.Otp
{
    public class OtpService(IMemoryCache cache, IEmailService emailService) : IOtpService
    {

        public async Task SendAndCacheOtpAsync(string email, string fullName)
        {
            // 1. Tạo mã
            string otp = new Random().Next(100000, 999999).ToString();

            // 2. Lưu vào Cache (Sử dụng IMemoryCache)
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            cache.Set($"OTP_{email}", otp, cacheOptions);

            // 3. Tạo/Cập nhật khóa phụ chặn gửi lại (60 giây)
            cache.Set($"ResendLock_{email}", true, TimeSpan.FromSeconds(60));

            // 4. Sử dụng EmailService để gửi
            string subject = "Xác nhận tài khoản StoreApp";
            string body = $"<h3>Chào {fullName},</h3><p>Mã OTP của bạn là: <b>{otp}</b></p>";

            await emailService.SendEmailAsync(email, subject, body);
        }

        public bool ValidateOtp(string email, string inputOtp)
        {
            if (cache.TryGetValue($"OTP_{email}", out string sendedOtp))
            {
                return sendedOtp == inputOtp;
            }
            return false;
        }
        public void ClearOtp(string email)
        {
            // Tạo đúng Key mà bạn đã dùng để lưu lúc Register/Resend
            string cacheKey = $"OTP_{email}";

            // Xóa bỏ Key này khỏi RAM ngay lập tức
            cache.Remove(cacheKey);
        }

        public bool IsResendLocked(string email)
        {
            return cache.TryGetValue($"ResendLock_{email}", out _);
        }
    }
}
