using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StoreApp.Application.Repository;
using StoreApp.Application.Service.Email;
using StoreApp.Application.Service.Payment;
using StoreApp.Application.Service.Security;
using StoreApp.Infrastructure.Adapter;
using StoreApp.Infrastructure.Adapter.Email;
using StoreApp.Infrastructure.Adapter.Security;
using StoreApp.Infrastructure.Data;
using System.Security.Claims;
using System.Text;

namespace StoreApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer("Server=SHIBATEO\\SQLEXPRESS;Database=StoreApp4;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
            });

            // Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IGRNRepository, GRNRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();

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
                            ValidateIssuerSigningKey = true,
                            RoleClaimType = ClaimTypes.Role
                        };
                    });

            // Application Services
            return services;
        }
    }
}
