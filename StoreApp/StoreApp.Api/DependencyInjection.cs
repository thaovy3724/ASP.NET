using StoreApp.Core;
using StoreApp.Infrastructure;

namespace StoreApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddCoreDI()
                .AddInfrastructureDI();
            // Application Services
            return services;
        }
    }
}
