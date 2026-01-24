using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer("Server=THANKS;Database=store_management;Trusted_Connection=True;TrustServerCertificate=True");
            });
            // Application Services
            return services;
        }
    }
}
