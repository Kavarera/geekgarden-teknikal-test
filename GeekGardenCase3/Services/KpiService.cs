using GeekGardenCase3.Data;
using GeekGardenCase3.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekGardenCase3.Services
{
    public class KpiService : IKpiService
    {
        private readonly AppDbContext context;

        public KpiService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<KpiSummary>> GetKpiSummariesAsync()
        {
            var sum = await context.Companies
                .Select(c=>new KpiSummary
                {
                    CompanyId = c.Id,
                    CompanyName = c.Name,
                    AverageKpiScore = c.Kpis.Average(k => k.Score)
                }).ToListAsync();

            return sum;
        }
    }
}
