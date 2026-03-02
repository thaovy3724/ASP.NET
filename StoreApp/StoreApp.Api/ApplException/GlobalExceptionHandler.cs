using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.Exceptions;
using StoreApp.Core.Exceptions;

namespace StoreApp.Api.ApplException
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, System.Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Lỗi xảy ra: {Message}", exception.Message);

            var (statusCode, title) = MapException(exception);
         
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            //if (exception is ValidationException validationException)
            //{
            //    problemDetails.Extensions["errors"] = validationException.Errors;
            //}

            var result = await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                Exception = exception,
                ProblemDetails = problemDetails
            });
            return result;
        }

        private static (int StatusCode, string Title) MapException(System.Exception exception) => exception switch
        {

            NotFoundException => (StatusCodes.Status404NotFound, "Không tìm thấy dữ liệu"),
            BadRequestException => (StatusCodes.Status400BadRequest, "Yêu cầu không hợp lệ"),
            ConflictException => (StatusCodes.Status409Conflict, "Dữ liệu bị xung đột"),
            DomainException => (StatusCodes.Status400BadRequest, "Lỗi nghiệp vụ"),
            //ValidationException => (StatusCodes.Status400BadRequest, "Lỗi xác thực dữ liệu"),

            //BaseException appEx => ((int)appEx.StatusCode, "Lỗi xử lý yêu cầu"),

            ArgumentNullException => (StatusCodes.Status400BadRequest, "Tham số không được để trống"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Tham số không hợp lệ"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Bạn không có quyền truy cập"),
            _ => (StatusCodes.Status500InternalServerError, "Đã xảy ra lỗi hệ thống ngoài ý muốn")
            
        };
    }
}
