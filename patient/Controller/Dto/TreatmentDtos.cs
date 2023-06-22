namespace PatientMs.Controller.Dto.TreatmentDto
{
    public struct TreatmentDto
    {
        public long id { get; init; }
        public string name { get; init; }
        public TreatmentProcess[] treatmentConsistsOf { get; init; }
        public Condition[] treatmentFor { get; init; }
        public decimal totalPrice { get; init; }
    }


    public struct TreatmentProcess
    {
        public long id { get; init; }
        public string type { get; init; }
        public decimal cost { get; init; }
        public string location { get; init; }
        public int minimalTimeInMinutes { get; init; }
        public int maximumTimeInMinutes { get; init; }
        public bool isAvailable { get; init; }
    }

    public struct Condition
    {
        public long id { get; init; }
        public string name { get; init; }
        public string description { get; init; }
    }

}
