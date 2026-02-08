using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreApp.Application.Repository;
using StoreApp.Infrastructure.Adapter;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer("Server=LAPTOP-Q5U1UG52\\SQLEXPRESS;Database=StoreApp;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
            });

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Application Services
            return services;
        }
    }
}
