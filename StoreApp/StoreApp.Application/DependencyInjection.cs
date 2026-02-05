using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace StoreApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            // Application Services

            // Đăng ký MediatR
            // Bạn nên trỏ vào Project chứa các Handler (thường là lớp Application)
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(StoreApp.Application.UseCases.OrderUseCase.Command.Create.CreateOrderHandler).Assembly);
            });
            return services;
        }
    }
}
