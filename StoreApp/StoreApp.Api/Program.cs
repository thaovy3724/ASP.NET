using SM.Infrastructure.Adapters.Payment.Config;
using StoreApp.Api;
using StoreApp.Api.BackgroundServices;
using StoreApp.Application.Repository;
using StoreApp.Api.ApplException;

var builder = WebApplication.CreateBuilder(args);

//==== SERVICE REGISTRATION ====//

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add application and infrastructure services (repositories, services, etc.)
builder.Services.AddAppDI(builder.Configuration);

// cau hinh vnpay & order
builder.Services.Configure<VnPayProperties>(builder.Configuration.GetSection("VnPay"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddHostedService<OrderAutoCancelService>();

// Global exception handler
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

//==== CONFIGURE HTTP PIPELINE ====//

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStatusCodePages();

app.Run();
