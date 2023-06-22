using PatientMs.Controller.Dto;
using PatientMs.Domain;

namespace PatientMs.Utils
{
    public static class BillingUtils
    {

        public static BillDto toDto(this Bill bill)
        {
            return new BillDto
            {
                Cost = bill.cost.Value,
                MedicalCondition = bill.condition.toDto(),
                InsurancePolicy = bill.appliedInsurance.ToDto()
            };
        }
    }
}
