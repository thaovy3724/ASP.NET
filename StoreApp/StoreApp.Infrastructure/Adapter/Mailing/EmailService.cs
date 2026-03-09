using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using StoreApp.Application.Common.Settings;
using StoreApp.Application.Ports;

namespace StoreApp.Infrastructure.Adapters.Mailing
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        // Inject EmailSettings đã cấu hình trong appsettings.json
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // 1. Khởi tạo đối tượng lá thư (MimeMessage)
            var email = new MimeMessage();

            // Người gửi (Lấy từ cấu hình)
            email.From.Add(MailboxAddress.Parse(_emailSettings.Email));

            // Người nhận
            email.To.Add(MailboxAddress.Parse(to));

            // Tiêu đề thư
            email.Subject = subject;

            // Nội dung thư (Sử dụng định dạng HTML để OTP trông đẹp hơn)
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            // 2. Sử dụng SmtpClient của MailKit để gửi
            using var smtp = new SmtpClient();
            try
            {
                // Kết nối tới Server (Gmail: smtp.gmail.com, Port: 587)
                await smtp.ConnectAsync(
                    _emailSettings.Host,
                    _emailSettings.Port,
                    SecureSocketOptions.StartTls
                );

                // Xác thực bằng Email và App Password
                await smtp.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);

                // Thực hiện gửi thư
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Log lỗi tại đây nếu gửi thất bại
                throw new Exception("Lỗi khi gửi Email: " + ex.Message);
            }
            finally
            {
                // Ngắt kết nối an toàn
                await smtp.DisconnectAsync(true);
            }
        }
    }
}