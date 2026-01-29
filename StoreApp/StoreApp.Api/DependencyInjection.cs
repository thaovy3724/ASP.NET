using StoreApp.Application;
using StoreApp.Infrastructure;
namespace StoreApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddApplicationDI()
                    .AddInfrastructureDI(); // chain calls
            
            return services;
        }
    }
}
