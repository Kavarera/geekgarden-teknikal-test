using GeekGardenCase2.Models;

namespace GeekGardenCase2.Utils
{
    public interface IJwtUtil
    {
        string GenerateToken(User user);
        int? ValidateToken(string token);
    }
}
