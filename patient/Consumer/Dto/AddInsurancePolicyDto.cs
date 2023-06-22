namespace PatientMs.Consumer.Dto
{
    public struct AddInsurancePolicyDto
    {
        public long patientId {  get; set; }
        public string Name { get; set; }
        public decimal Coverage { get; set; }
        public decimal Deductible { get; set; }
    }
}
