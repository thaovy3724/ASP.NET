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
            string subject = "Mã xác thực OTP - StoreApp";

            string body = $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #eee; padding: 20px;'>
                <h2 style='color: #333;'>Xác nhận tài khoản StoreApp</h2>
                <p>Chào <b>{fullName}</b>,</p>
                <p>Bạn vừa thực hiện yêu cầu xác thực tài khoản. Vui lòng sử dụng mã OTP dưới đây:</p>
        
                <div style='background-color: #f8f9fa; padding: 15px; text-align: center; border-radius: 5px; margin: 20px 0;'>
                    <span style='font-size: 24px; font-weight: bold; letter-spacing: 5px; color: #007bff;'>{otp}</span>
                </div>

                <p style='color: #d9534f; font-weight: bold;'>
                    ⚠️ Lưu ý: Mã này chỉ có hiệu lực trong vòng 5 phút.
                </p>
        
                <p>Sau thời gian này, mã sẽ tự động hết hạn và bạn sẽ cần yêu cầu mã mới nếu chưa hoàn tất xác thực.</p>
                <hr style='border: 0; border-top: 1px solid #eee;' />
                <p style='font-size: 12px; color: #888;'>Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này.</p>
            </div>";

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
