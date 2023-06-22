using Microsoft.AspNetCore.Mvc;
using PatientMs.Service;

namespace PatientMs.Controller
{
    [Route("/cost")]
    [Produces("application/json")]
    [ApiController]
    public class CostController : ControllerBase
    {
        private ICostService service { get; init; }

        public CostController(ICostService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCostSoFar()
        {
            return Ok(service.GetCostsSoFar());
        }

    }
}
