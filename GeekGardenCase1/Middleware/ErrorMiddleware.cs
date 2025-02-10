namespace GeekGardenCase1.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorMiddleware> logger;

        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> log)
        {
            this.next = next;
            logger = log;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal Server Error");
            }
        }

    }
    public static class ErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorMiddleware>();
        }
    }
}
