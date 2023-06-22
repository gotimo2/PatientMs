using PatientMs.Value;

namespace PatientMs.Domain
{
    public class Bill
    {
        public Cost cost { get; init; }

        public InsurancePolicy appliedInsurance { get; init; }

        public MedicalCondition condition { get; init; }

    }
}
