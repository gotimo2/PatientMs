using Microsoft.AspNetCore.Mvc;
using PatientMs.Controller.Dto;
using PatientMs.Service;
using PatientMs.Utils;

namespace PatientMs.Controller
{
    [Route("/{id}/insurance")]
    [Produces("application/json")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private IInsuranceService service { get; init; }
        private ICostService costService { get; init; }

        public InsuranceController(IInsuranceService service, ICostService costService)
        {
            this.costService = costService;
            this.service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetInsurance(int id)
        {
            await costService.IncrementCosts((decimal)0.25);
            return Ok((await service.GetInsurancePolicies(id)).ToDtos());
        }


        [HttpPost]
        public async Task<IActionResult> AddInsurance(int id, [FromBody] InsurancePolicyDto insurancePolicyDto)
        {
            await costService.IncrementCosts((decimal)0.5);
            return Ok((await service.AddInsurancePolicy(id, insurancePolicyDto.ToInsurancePolicy())).ToDtos());

        }
    }
}
