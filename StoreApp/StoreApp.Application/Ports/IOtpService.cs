using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.Ports
{
    public interface IOtpService
    {
        // Gửi mã OTP và lưu vào Cache
        Task SendAndCacheOtpAsync(string email, string fullName);

        // Kiểm tra xem mã người dùng nhập có khớp với Cache không
        bool ValidateOtp(string email, string inputOtp);

        // Xóa mã sau khi đã Verify thành công
        void ClearOtp(string email);
        bool IsResendLocked(string email);
    }
}
