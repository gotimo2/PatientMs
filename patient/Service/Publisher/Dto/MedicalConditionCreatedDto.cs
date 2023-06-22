namespace PatientMs.Service.Publisher.Dto
{
    public struct MedicalConditionCreatedDto
    {
        public long patientId { get; set; }
        public Guid conditionId { get; set; }
    }
}
