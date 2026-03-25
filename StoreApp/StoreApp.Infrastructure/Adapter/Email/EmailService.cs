using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using StoreApp.Application.Service.Email;

namespace StoreApp.Infrastructure.Adapter.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration config)
        {
            _configuration = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // 1. Khởi tạo đối tượng lá thư (MimeMessage)
            var email = new MimeMessage();

            // Người gửi (Lấy từ cấu hình)
            email.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:Email"]));

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
                    _configuration["EmailSettings:Host"],
                    int.Parse(_configuration["EmailSettings:Port"]),
                    SecureSocketOptions.StartTls
                );

                // Xác thực bằng Email và App Password
                await smtp.AuthenticateAsync(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Passwords"]);

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