namespace StoreApp.Api
{
    public class LoggingMiddleware
    {
        protected readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Log request details
            Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
            await _next(context); // Call the next middleware in the pipeline
            // Log response details
            Console.WriteLine($"Response: {context.Response.StatusCode}");
        }
    }
}
