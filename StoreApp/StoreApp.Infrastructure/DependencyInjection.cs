using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StoreApp.Application.Repository;
using StoreApp.Infrastructure.Adapter;
using StoreApp.Infrastructure.Data;
using System.Text;

namespace StoreApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer("Server=LAPTOP-Q5U1UG52\\SQLEXPRESS;Database=StoreApp;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
            });

            // Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IGRNRepository, InventoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Security Services
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = configuration["AppSettings:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = configuration["AppSettings:Audience"],
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]!)),
                            ValidateIssuerSigningKey = true
                        };
                    });

            // Application Services
            return services;
        }
    }
}
