using PatientMs.Controller.Dto;
using PatientMs.Domain;
using PatientMs.Value.Insurance;

namespace PatientMs.Utils
{
    public static class InsuranceUtils
    {
        public static InsurancePolicyDto ToDto(this InsurancePolicy policy)
        {
            return new InsurancePolicyDto
            {
                Coverage = policy.Coverage,
                Deductible = policy.Deductible,
                Name = policy.Name
            };
        }

        public static InsurancePolicy ToInsurancePolicy(this InsurancePolicyDto dto)
        {
            return new InsurancePolicy(dto.Name, new Coverage(dto.Coverage), new Deductible(dto.Deductible));
        }

        public static InsurancePolicy ToInsurancePolicy(this Consumer.Dto.AddInsurancePolicyDto dto)
        {
            return new InsurancePolicy(dto.Name, new Coverage(dto.Coverage), new Deductible(dto.Deductible));
        }

        public static List<InsurancePolicyDto> ToDtos(this List<InsurancePolicy> policies)
        {
            List<InsurancePolicyDto> output = new List<InsurancePolicyDto>();
            foreach (var policy in policies)
            {
                output.Add(policy.ToDto());
            }
            return output;
        }

    }
}
