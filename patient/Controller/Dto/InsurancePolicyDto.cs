namespace PatientMs.Controller.Dto
{
    public struct InsurancePolicyDto
    {
        public string Name { get; init; }
        public Decimal Coverage { get; init; }
        public Decimal Deductible { get; init; }
    }
}
