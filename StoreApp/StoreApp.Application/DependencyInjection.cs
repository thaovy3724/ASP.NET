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
            return services;
        }
    }
}
