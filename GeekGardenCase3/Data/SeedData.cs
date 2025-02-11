using GeekGardenCase3.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekGardenCase3.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (context.Companies.Any())
            {
                return;
            }

            var companies = new List<Company>
            {
                new Company
                {
                    Name = "Company A",
                    Kpis = new List<Kpi>
                    {
                        new Kpi { Score = 85.5m, Date = new DateTime(2025, 1, 1) },
                        new Kpi { Score = 90.0m, Date = new DateTime(2025, 2, 1) }
                    }
                },
                new Company
                {
                    Name = "Company B",
                    Kpis = new List<Kpi>
                    {
                        new Kpi { Score = 75.0m, Date = new DateTime(2025, 1, 1) },
                        new Kpi { Score = 80.0m, Date = new DateTime(2025, 2, 1) }
                    }
                }
            };

            context.Companies.AddRange(companies);
            context.SaveChanges();
        }
    }
}
