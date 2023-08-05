using System.Net;

namespace CutInLine.Models.GlobalException
{
    public static class GlobalExceptionMiddlewarePrincipal
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }

    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public GlobalExceptionMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            _logger = loggerFactory.CreateLogger("GlobalException");//Lets create new console logger overriding default logging
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            // var unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();

            try
            {
                // Call the next delegate/ middleware in the pipeline
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                await HandleGlobalExceptionAsync(httpContext, ex);

                // unitOfWork.Rollback();
            }
            finally
            {
                // unitOfWork.Dispose();
            }
        }

        private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            return context.Response.WriteAsync(new GlobalErrorDetails()
            {
                Success = false,
                Message = exception.Message,
            }.ToString());
        }
    }
}
