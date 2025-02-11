using GeekGardenCase3.Models;

namespace GeekGardenCase3.Services
{
    public interface IKpiService
    {
        Task<List<KpiSummary>> GetKpiSummariesAsync();
    }
}
