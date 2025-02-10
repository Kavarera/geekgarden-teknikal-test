using GeekGardenCase1.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekGardenCase1.Data
{
    public class EmailDbContext : DbContext
    {
        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options)
        {
        }
        public DbSet<Email> Emails { get; set; }
    }
}
