using Microsoft.Extensions.Caching.Memory;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Service.Email;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter.Email
{
    public class OtpService(IMemoryCache cache, IEmailService emailService) : IOtpService
    {
        private const int OtpExpiresMinutes = 5;
        private const int ResendLockSeconds = 60;
        private const int MaxSendPerHour = 5;

        private sealed record PendingOtpData(string OTP, User UserData);

        private sealed class OtpRateLimitCounter
        {
            public int Count { get; set; }
            public DateTime ExpireAt { get; init; }
        }

        public async Task SendAndCacheOtpAsync(User user)
        {
            CheckRateLimit(user.Username);

            await SendOTP(user);
        }

        public async Task ResendAndCachedOtpAsync(string userName)
        {
            CheckRateLimit(userName);

            if (cache.TryGetValue(VerifyKey(userName), out PendingOtpData? data)
                && data?.UserData is not null)
            {
                await SendOTP(data.UserData);
                return;
            }

            throw new BadRequestException("Bạn chưa thực hiện đăng ký trước đó hoặc OTP đã hết hạn.");
        }

        private async Task SendOTP(User user)
        {
            var email = NormalizeEmail(user.Username);

            string otp = Random.Shared.Next(100000, 1000000).ToString();

            var cacheData = new PendingOtpData(otp, user);

            cache.Set(
                VerifyKey(email),
                cacheData,
                TimeSpan.FromMinutes(OtpExpiresMinutes)
            );

            // Set khóa 60 giây và tăng bộ đếm 1 giờ trước khi gọi SMTP.
            // Như vậy nếu bị giới hạn thì sẽ bị chặn từ trước, không gọi SMTP.
            IncreaseRateLimitCounter(email);

            string subject = "Mã xác thực OTP - StoreApp";

            string body = $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #eee; padding: 20px;'>
                <h2 style='color: #333;'>Xác nhận tài khoản StoreApp</h2>
                <p>Chào <b>{user.FullName}</b>,</p>
                <p>Bạn vừa thực hiện yêu cầu xác thực tài khoản. Vui lòng sử dụng mã OTP dưới đây:</p>

                <div style='background-color: #f8f9fa; padding: 15px; text-align: center; border-radius: 5px; margin: 20px 0;'>
                    <span style='font-size: 24px; font-weight: bold; letter-spacing: 5px; color: #007bff;'>{otp}</span>
                </div>

                <p style='color: #d9534f; font-weight: bold;'>
                    ⚠️ Lưu ý: Mã này chỉ có hiệu lực trong vòng {OtpExpiresMinutes} phút.
                </p>

                <p>Sau thời gian này, mã sẽ tự động hết hạn và bạn sẽ cần yêu cầu mã mới nếu chưa hoàn tất xác thực.</p>
                <hr style='border: 0; border-top: 1px solid #eee;' />
                <p style='font-size: 12px; color: #888;'>Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email này.</p>
            </div>";

            await emailService.SendEmailAsync(email, subject, body);
        }

        private void CheckRateLimit(string email)
        {
            email = NormalizeEmail(email);

            // Điều kiện 1: mỗi email chỉ được gửi 1 lần trong 60 giây
            if (cache.TryGetValue(ResendLockKey(email), out _))
            {
                throw new TooManyRequestsException("Vui lòng đợi 60 giây trước khi yêu cầu mã OTP mới.");
            }

            // Điều kiện 2: tối đa 5 lần trong 1 giờ
            if (cache.TryGetValue(HourlyCounterKey(email), out OtpRateLimitCounter? counter)
                && counter is not null
                && counter.Count >= MaxSendPerHour)
            {
                throw new TooManyRequestsException("Email này đã yêu cầu OTP quá 5 lần trong 1 giờ. Vui lòng thử lại sau.");
            }
        }

        private void IncreaseRateLimitCounter(string email)
        {
            email = NormalizeEmail(email);

            var counterKey = HourlyCounterKey(email);

            if (!cache.TryGetValue(counterKey, out OtpRateLimitCounter? counter) || counter is null)
            {
                counter = new OtpRateLimitCounter
                {
                    Count = 1,
                    ExpireAt = DateTime.Now.AddHours(1)
                };
            }
            else
            {
                counter.Count++;
            }

            cache.Set(
                counterKey,
                counter,
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(counter.ExpireAt)
            );

            cache.Set(
                ResendLockKey(email),
                true,
                TimeSpan.FromSeconds(ResendLockSeconds)
            );
        }

        public User? ValidateOtp(string email, string inputOtp)
        {
            email = NormalizeEmail(email);

            if (cache.TryGetValue(VerifyKey(email), out PendingOtpData? data)
                && data is not null
                && data.OTP == inputOtp)
            {
                return data.UserData;
            }

            return null;
        }

        public void ClearOtp(string email)
        {
            email = NormalizeEmail(email);

            cache.Remove(VerifyKey(email));
            cache.Remove(ResendLockKey(email));
        }

        public bool IsResendLocked(string email)
        {
            email = NormalizeEmail(email);
            return cache.TryGetValue(ResendLockKey(email), out _);
        }

        private static string NormalizeEmail(string email)
        {
            return email.Trim().ToLowerInvariant();
        }

        private static string VerifyKey(string email)
        {
            return $"Verify_{NormalizeEmail(email)}";
        }

        private static string ResendLockKey(string email)
        {
            return $"OtpResendLock_{NormalizeEmail(email)}";
        }

        private static string HourlyCounterKey(string email)
        {
            return $"OtpHourlyCounter_{NormalizeEmail(email)}";
        }
    }
}