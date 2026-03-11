using SM.Infrastructure.Adapters.Payment.Config;
using StoreApp.Api;
using StoreApp.Api.ApplException;
using StoreApp.Api.BackgroundServices;
using StoreApp.Application.Common.Settings;
using StoreApp.Application.Service.Payment;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//==== SERVICE REGISTRATION ====//

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// để enum (Role…) trả về dạng "Admin", "Staff", "Client" thay vì số.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// CORS configuration to allow requests from RazorFE
builder.Services.AddCors(options =>
{
    options.AddPolicy("RazorFE", policy =>
        policy.WithOrigins("https://localhost:7235", "http://localhost:5293")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// cau hinh vnpay & order
builder.Services.Configure<VnPayProperties>(builder.Configuration.GetSection("VnPay"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddHostedService<OrderAutoCancelService>();

// Global exception handler
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add application and infrastructure services (repositories, services, etc.)
builder.Services.AddAppDI(builder.Configuration);
// Cấu hình EmailSettings để có thể inject vào các service cần thiết

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Đăng ký MemoryCache để lưu mã OTP tạm thời
builder.Services.AddMemoryCache();

//==== CONFIGURE HTTP PIPELINE ====//

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseStaticFiles();   // để có thể truy cập vào wwwroot (chứa ảnh sản phẩm)

app.UseCors("RazorFE");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStatusCodePages();

app.Run();
