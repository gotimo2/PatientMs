using Newtonsoft.Json;
using PatientMs.Value;

namespace PatientMs.Domain
{

    public class Patient
    {
        public long id { get; private init; }

        [JsonProperty] //ik had het meteen moeten zien 
        private List<InsurancePolicy> _insurancePolicies = new List<InsurancePolicy>();

        [JsonProperty]
        private List<MedicalCondition> _medicalConditions = new List<MedicalCondition>();


        public Adress Adress { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MedicalProfile MedicalProfile { get; init; }


        public List<InsurancePolicy> InsurancePolicies { get { return new List<InsurancePolicy>(_insurancePolicies); } }

        public List<MedicalCondition> MedicalConditions { get { return new List<MedicalCondition>(_medicalConditions); } }

        public Patient(FirstName firstName, LastName lastName, MedicalProfile medicalProfile, Adress adress)
        {
            FirstName = firstName.Value;
            LastName = lastName.Value;
            MedicalProfile = medicalProfile;
            Adress = adress;
        }

        [JsonConstructor]
        private Patient() { }

        public void CopyValidAttributes(Patient other)
        {
            if (other.FirstName != null) { FirstName = other.FirstName; }
            if (other.LastName != null) { LastName = other.LastName; }
            if (other.Adress != null)
            {
                if (other.Adress.PostalCode != null) { Adress.PostalCode = other.Adress.PostalCode; }
                if (other.Adress.Street != null) { Adress.Street = other.Adress.Street; }
                if (other.Adress.HouseNumber != null) { Adress.HouseNumber = other.Adress.HouseNumber; }
            }
            if (other.MedicalProfile != null)
            {
                if (other.MedicalProfile.Length != 0) { MedicalProfile.Length = other.MedicalProfile.Length; }
                if (other.MedicalProfile.Weight != 0) { MedicalProfile.Weight = other.MedicalProfile.Weight; }
                MedicalProfile.BloodType = other.MedicalProfile.BloodType;
            }

        }


        public void AddInsurancePolicy(InsurancePolicy policy)
        {
            _insurancePolicies.Add(policy);
        }

        public void AddMedicalCondition(MedicalCondition condition)
        {
            _medicalConditions.Add(condition);
        }

        public void RemoveMedicalCondition(MedicalCondition condition)
        {
            _medicalConditions.Remove(condition);
        }

    }
}
