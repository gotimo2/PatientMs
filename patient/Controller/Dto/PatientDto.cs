namespace PatientMs.Controller.Dto
{
    public struct PatientDto
    {

        public long id { get; init; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public AdressDto adress { get; set; }
        public MedicalProfileDto medicalProfile { get; set; }

    }
}
