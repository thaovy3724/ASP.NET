using SM.Infrastructure.Adapters.Payment.Config;
using StoreApp.Api;
using StoreApp.Api.BackgroundServices;
using StoreApp.Application.Repository;
using StoreApp.Api.ApplException;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAppDI(builder.Configuration);
builder.Services.AddAppDI();

// cau hinh vnpay & order
builder.Services.Configure<VnPayProperties>(builder.Configuration.GetSection("VnPay"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddHostedService<OrderAutoCancelService>();

var app = builder.Build();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
var app = builder.Build();
app.UseExceptionHandler();
// Configure the HTTP request pipeline.
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
