using GeekGardenCase2.Data;
using GeekGardenCase2.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekGardenCase2.Services
{
    public class UserServices : IUserService
    {
        private readonly AppDbContext context;

        private readonly ILogger<UserServices> logger;

        public UserServices(AppDbContext context, ILogger<UserServices> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await context.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                logger.LogWarning("User not found: {Username}", username);
                return null;
            }
            if (!VerifyPassword(password, user.Password))
            {
                logger.LogWarning("Password verification failed for user: {Username}", username);
                return null;
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await context.Users.Include(u => u.Role).Include(u=>u.Department).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllByDepartment(int curUser)
        {
            var temp = await GetById(curUser);
            if (temp.Department == null)
                return Enumerable.Empty<User>();
            return await context.Users
                .Include(u => u.Department)
                .Where(u => u.Department.Id == temp.Department.Id)
                .ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            var user = await context.Users.Include(u => u.Role).
                Include(u=>u.Department).SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return null;
            return user;
        }

        public async Task<User> GetByUsername(string username)
        {
            var user = await context.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            return user;
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            logger.LogInformation("Hashed password: {0}", hashedPassword);
            logger.LogInformation("Stored hash: {0}", storedHash);
            return hashedPassword == storedHash;
        }
    }
}
