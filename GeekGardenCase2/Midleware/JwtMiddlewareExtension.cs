namespace GeekGardenCase2.Midleware
{
    public static class JwtMiddlewareExtension
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
