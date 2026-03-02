using StoreApp.Application;
using StoreApp.Infrastructure;
namespace StoreApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureDI(configuration)
                    .AddApplicationDI(); // chain calls
            
            return services;
        }
    }
}
