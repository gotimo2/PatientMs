using Microsoft.AspNetCore.Mvc;
using PatientMs.Controller.Dto;
using PatientMs.Service;
using PatientMs.Utils;

namespace PatientMs.Controller
{
    [ApiController]
    [Route("/{id}/condition")]
    [Produces("application/json")]
    public class MedicalConditionController : ControllerBase
    {
        private IMedicalConditionService service { get; init; }
        private ICostService costService { get; init; }


        public MedicalConditionController(IMedicalConditionService service, ICostService costService)
        {
            this.costService = costService;
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicalConditions(int id)
        {
            await costService.IncrementCosts((decimal)0.15);
            return Ok((await service.GetMedicalConditions(id)).toDtos());
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicalCondition(int id, [FromBody] MedicalConditionDto dto)
        {
            await costService.IncrementCosts((decimal)0.35);
            return Ok((await service.AddMedicalCondition(id, dto.toMedicalCondition())).toDtos());
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveMedicalCondition(int id, [FromBody] MedicalConditionDto dto)
        {
            await costService.IncrementCosts((decimal)0.5);
            return Ok((await service.RemoveMedicalConditon(id, dto.toMedicalCondition())).toDtos());

        }

        [HttpPut]
        public async Task<IActionResult> FinalizeTreatment(int id, [FromBody] FinalizingDto Dto)
        {
            await costService.IncrementCosts((decimal)1.5);
            return Ok((await service.FinalizeTreatment(id, Dto.ConditionId, Dto.TreatmentId)).toDto());
        }


    }
}
