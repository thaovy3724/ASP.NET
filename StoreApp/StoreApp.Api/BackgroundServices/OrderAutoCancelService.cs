using MediatR;
using Microsoft.Extensions.Options;
using SM.Infrastructure.Adapters.Payment.Config;
using StoreApp.Application.Repository;
using StoreApp.Application.UseCases.OrderUseCase.Command.Cancel;

namespace StoreApp.Api.BackgroundServices;

public class OrderAutoCancelService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderAutoCancelService> _logger; // 1. Inject Logger
    private readonly VnPayProperties _vnPayConfig;
    public OrderAutoCancelService(IServiceProvider serviceProvider, ILogger<OrderAutoCancelService> logger, IOptions<VnPayProperties> vnPayConfig)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _vnPayConfig = vnPayConfig.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Order Auto Cancel Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // 2. TẠO SCOPE
                // Dùng using để giải phóng tài nguyên
                using (var scope = _serviceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var orderRepo = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

                    // 3. Logic tìm đơn quá hạn
                    // Lấy thời điểm hiện tại trừ đi expireDate của vnPayProperties
                    int timeoutMinutes = _vnPayConfig.PaymentTimeout;
                    var timeLimit = DateTime.UtcNow.AddMinutes(-timeoutMinutes);

                    var expiredOrders = await orderRepo.GetListExpiredOrders(timeLimit);

                    if (expiredOrders.Any())
                    {
                        _logger.LogInformation($"Tìm thấy {expiredOrders.Count} đơn hàng treo cần hủy.");

                        foreach (var order in expiredOrders)
                        {
                            try
                            {
                                // Gửi Command để xử lý (IsSuccess = false -> Hủy đơn & Hoàn kho)
                                // Lưu ý: TransactionId null vì không có giao dịch VNPay
                                var command = new CancelOrderCommand(order.Id, null);
                                await mediator.Send(command);

                                _logger.LogInformation($"Đã hủy tự động đơn: {order.Id}");
                            }
                            catch (Exception ex)
                            {
                                // Try-catch lồng: Để nếu 1 đơn lỗi thì các đơn sau vẫn chạy tiếp
                                _logger.LogError(ex, $"Lỗi khi hủy đơn {order.Id}");
                            }
                        }
                    }
                }
                // Chờ 1 phút rồi quét lại (Đừng để nhanh quá tốn tài nguyên)
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            catch (Exception ex)
            {
                // Try-catch tổng: Để service không bao giờ chết
                _logger.LogError(ex, "Lỗi nghiêm trọng trong vòng lặp Background Service");
            }

        }
    }
}