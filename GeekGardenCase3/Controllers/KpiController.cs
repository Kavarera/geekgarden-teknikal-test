using GeekGardenCase3.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekGardenCase3.Controllers
{
    [Route("api/kpi-summary")]
    [ApiController]
    public class KpiController : ControllerBase
    {
        private readonly IKpiService kpiService;
        public KpiController(IKpiService kpiService)
        {
            this.kpiService = kpiService;
        }
        [HttpGet]
        public async Task<IActionResult> GetKpiSummaries()
        {
            var summaries = await kpiService.GetKpiSummariesAsync();
            return Ok(summaries);
        }
    }
}
