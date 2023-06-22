using Microsoft.AspNetCore.Mvc;
using PatientMs.Controller.Dto;
using PatientMs.Service;
using PatientMs.Utils;
using System.Net.Mime;

namespace PatientMs.Controller
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/")]
    public class PatientController : ControllerBase
    {
        private IPatientService service { get; init; }

        private ICostService costService { get; init; }

        public PatientController(IPatientService patientService, ICostService costService)
        {
            this.costService = costService;
            service = patientService;
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            return Ok((await service.GetPatient(id)).toDto());
        }

        [HttpPost]
        public async Task<IActionResult> PostPatient([FromBody] PatientDto patientDto)
        {
            await costService.IncrementCosts((decimal)1.25);
            return Ok((await service.NewPatient(patientDto.toPatient())).toDto());

        }

        [HttpPut("/{id}")]
        public async Task<IActionResult> PutPatient(int id, [FromBody] PatientDto patientDto)
        {
            await costService.IncrementCosts((decimal)0.75);
            return Ok((await service.UpdatePatient(id, patientDto.toPatient())).toDto());

        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            await costService.IncrementCosts((decimal)0.75);
            await service.DeletePatient(id);
            return Ok();

        }


    }
}
