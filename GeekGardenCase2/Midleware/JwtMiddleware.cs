
using GeekGardenCase2.Services;
using GeekGardenCase2.Utils;

namespace GeekGardenCase2.Midleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;


        public JwtMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtil jwtUtil)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
            var userId = jwtUtil.ValidateToken(token);
            if (userId != null)
            {
                context.Items["User"] = await userService.GetById(userId.Value);
            }
            await next(context);
        }
    }
}
