namespace PatientMs.Controller.Dto
{
    public struct BillDto
    {
        public MedicalConditionDto MedicalCondition { get; set; }
        public InsurancePolicyDto InsurancePolicy { get; set; }
        public decimal Cost { get; set; }
    }
}
