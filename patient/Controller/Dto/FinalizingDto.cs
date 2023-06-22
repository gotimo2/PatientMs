namespace PatientMs.Controller.Dto
{
    public struct FinalizingDto
    {
        public Guid ConditionId { get; init; }
        public Guid TreatmentId { get; init; }
    }
}
