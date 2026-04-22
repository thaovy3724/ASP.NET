using MediatR;
using Microsoft.Extensions.Logging;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Application.Service.Email;
using System.Globalization;
using System.Net;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Confirm
{
    public class ConfirmOrderHandler(
        IOrderRepository orderRepository,
        IUserRepository userRepository,
        IEmailService emailService,
        ILogger<ConfirmOrderHandler> logger) : IRequestHandler<ConfirmOrderCommand, Unit>
    {
        public async Task<Unit> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            // Lấy order kèm Items để tính được TotalAmount khi gửi mail
            var order = await orderRepository.GetByIdWithItems(request.Id);

            if (order is null)
            {
                throw new NotFoundException($"Không tìm thấy đơn hàng với Id: {request.Id}");
            }

            // Xác nhận đơn hàng
            order.ConfirmOrder(request.StaffId!.Value);

            // Lưu trạng thái Confirmed xuống DB trước
            await orderRepository.Update(order);

            // Gửi email thông báo sau khi xác nhận thành công
            // Nếu gửi mail lỗi thì chỉ log, không rollback trạng thái đơn hàng
            await SendOrderConfirmedEmailAsync(order, cancellationToken);

            return Unit.Value;
        }

        private async Task SendOrderConfirmedEmailAsync(StoreApp.Core.Entities.Order order, CancellationToken cancellationToken)
        {
            try
            {
                // Username của user trong project đang là email
                var customer = await userRepository.GetById(order.CustomerId);

                if (customer is null)
                {
                    logger.LogWarning(
                        "Không gửi được email xác nhận đơn {OrderId} vì không tìm thấy customer {CustomerId}.",
                        order.Id,
                        order.CustomerId
                    );

                    return;
                }

                var subject = $"StoreApp - Đơn hàng {ShortId(order.Id)} đã được xác nhận";
                var body = BuildConfirmedOrderEmailBody(order, customer.FullName);

                await emailService.SendEmailAsync(customer.Username, subject, body);
            }
            catch (Exception ex)
            {
                // Email là tác vụ phụ trợ.
                // Xác nhận đơn hàng đã thành công thì không rollback chỉ vì gửi mail lỗi.
                logger.LogError(
                    ex,
                    "Xác nhận đơn {OrderId} thành công nhưng gửi email thông báo thất bại.",
                    order.Id
                );
            }
        }

        private static string BuildConfirmedOrderEmailBody(StoreApp.Core.Entities.Order order, string customerName)
        {
            var vi = new CultureInfo("vi-VN");

            var safeCustomerName = WebUtility.HtmlEncode(customerName);
            var safeOrderId = WebUtility.HtmlEncode(order.Id.ToString());
            var safeStatus = WebUtility.HtmlEncode(order.OrderStatus.ToString());

            var totalText = string.Format(vi, "{0:N0} ₫", order.TotalAmount);
            var confirmedTime = order.UpdatedAt.ToString("dd/MM/yyyy HH:mm", vi);

            return $@"
        <div style='font-family: Arial, sans-serif; max-width: 640px; margin: 0 auto; border: 1px solid #eee; border-radius: 10px; padding: 20px;'>
            <h2 style='margin-top: 0; color: #2563eb;'>Đơn hàng đã được xác nhận</h2>

            <p>Xin chào <b>{safeCustomerName}</b>,</p>

            <p>Đơn hàng của bạn đã được nhân viên StoreApp xác nhận thành công.</p>

            <table style='width: 100%; border-collapse: collapse; margin-top: 16px;'>
                <tr>
                    <td style='padding: 8px; border: 1px solid #eee;'>Mã đơn</td>
                    <td style='padding: 8px; border: 1px solid #eee;'><b>{safeOrderId}</b></td>
                </tr>

                <tr>
                    <td style='padding: 8px; border: 1px solid #eee;'>Tổng tiền</td>
                    <td style='padding: 8px; border: 1px solid #eee;'><b>{totalText}</b></td>
                </tr>

                <tr>
                    <td style='padding: 8px; border: 1px solid #eee;'>Trạng thái mới</td>
                    <td style='padding: 8px; border: 1px solid #eee;'><b>{safeStatus}</b></td>
                </tr>

                <tr>
                    <td style='padding: 8px; border: 1px solid #eee;'>Thời gian xác nhận</td>
                    <td style='padding: 8px; border: 1px solid #eee;'>{confirmedTime}</td>
                </tr>
            </table>

            <p style='margin-top: 16px;'>Cảm ơn bạn đã mua hàng tại StoreApp.</p>

            <hr style='border: 0; border-top: 1px solid #eee;' />

            <p style='font-size: 12px; color: #888;'>
                Đây là email tự động, vui lòng không trả lời email này.
            </p>
        </div>";
        }

        private static string ShortId(Guid id)
        {
            var text = id.ToString("N");
            return text.Length > 8 ? text[..8] : text;
        }
    }
}