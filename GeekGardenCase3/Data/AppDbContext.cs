using Microsoft.EntityFrameworkCore;

namespace GeekGardenCase3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Company> Companies { get; set; }
        public DbSet<Models.Kpi> Kpis { get; set; }
    }
}
