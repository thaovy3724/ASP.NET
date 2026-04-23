using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.UseCases.OrderUseCase.Command.Cancel;
using StoreApp.Infrastructure.Adapter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StoreApp.Api.BackgroundServices
{
    public class LockedUserWhenFailedLoginMore5Time : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<LockedUserWhenFailedLoginMore5Time> _logger;

        public LockedUserWhenFailedLoginMore5Time(IServiceProvider serviceProvider, ILogger<LockedUserWhenFailedLoginMore5Time> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Locked users when login failed more 5 times");
            while (!stoppingToken.IsCancellationRequested) {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                        var users = await userRepo.GetAll();
                        foreach (var user in users)
                        {
                            // Only consider locked users and make sure lockedDate has value
                            if (user.lockoutEnd == 1 && user.lockedDate.HasValue)
                            {
                                DateTime current = DateTime.Now;
                                // keep 15 minutes lock window (adjust as needed)
                                DateTime expiredLock = user.lockedDate.Value.AddMinutes(1);
                                if (current >= expiredLock)
                                {
                                    user.lockoutEnd = 0;
                                    user.failedLoginCount = 0;
                                    user.lockedDate = null;
                                    await userRepo.Update(user);
                                    _logger.LogInformation($"User {user.Username} has been unlocked after 15 minutes.");
                                }
                            }
                        }
                        
                    }
                    // Chờ 1 phút rồi quét lại (Đừng để nhanh quá tốn tài nguyên)
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                } catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while locking users.");
                }
            }
        }
    }
}
