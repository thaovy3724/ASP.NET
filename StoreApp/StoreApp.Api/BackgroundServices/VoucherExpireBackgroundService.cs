using StoreApp.Application.Repository;

namespace StoreApp.Api.BackgroundServices
{
    public class VoucherExpireBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<VoucherExpireBackgroundService> logger
    ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("VoucherExpireBackgroundService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = scopeFactory.CreateScope();

                    var voucherRepository = scope.ServiceProvider.GetRequiredService<IVoucherRepository>();

                    var now = DateTime.UtcNow.AddHours(7);
                    var expiredVouchers = await voucherRepository.GetExpiredActiveVouchers(now);

                    if (expiredVouchers.Count > 0)
                    {
                        foreach (var voucher in expiredVouchers)
                        {
                            voucher.Inactivate();
                        }

                        await voucherRepository.SaveChangesAsync();

                        logger.LogInformation("Inactive {Count} expired vouchers.", expiredVouchers.Count);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while expiring vouchers.");
                }

                // Demo: chạy mỗi 2 phút.
                // Khi làm thật có thể đổi thành TimeSpan.FromDays(1).
                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }
    }
}