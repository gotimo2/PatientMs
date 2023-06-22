namespace PatientMs.Consumer.Dto
{
    public struct AddMedicalConditionDto
    {
        public long patientId {  get; set; } 
        public Guid id { get; set; }
    }
}
