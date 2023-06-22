
using Newtonsoft.Json;
using PatientMs.Value.MedicalDetails;

namespace PatientMs.Domain
{

    public class MedicalProfile
    {
        private long id { get; init; }

        public double Length { get; set; }

        public double Weight { get; set; }

        public BloodType BloodType { get; set; }

        public MedicalProfile(Length length, Weight weight, BloodType bloodType)
        {
            Length = length.Value;
            Weight = weight.Value;
            BloodType = bloodType;
        }

        [JsonConstructor]
        private MedicalProfile() { }

    }
}
