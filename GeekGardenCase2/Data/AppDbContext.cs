using GeekGardenCase2.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace GeekGardenCase2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<User>()
                .HasIndex(e=>e.Username).IsUnique();

            model.Entity<Department>().HasIndex(e => e.Name).IsUnique();

            model.Entity<Role>().HasIndex(e => e.Name).IsUnique();

            base.OnModelCreating(model);
        }
    }
}
