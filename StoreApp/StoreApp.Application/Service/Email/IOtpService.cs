using StoreApp.Core.Entities;

namespace StoreApp.Application.Service.Email
{
    public interface IOtpService
    {
        // Gửi mã OTP và lưu vào Cache
        Task SendAndCacheOtpAsync(User user);
        Task ResendAndCachedOtpAsync(string userName);

        // Kiểm tra xem mã người dùng nhập có khớp với Cache không
        User? ValidateOtp(string email, string inputOtp);

        // Xóa mã sau khi đã Verify thành công
        void ClearOtp(string email);
        bool IsResendLocked(string email);
    }
}
