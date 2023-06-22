using PatientMs.Controller.Dto;
using PatientMs.Domain;
using PatientMs.Value;
using PatientMs.Value.MedicalDetails;

namespace PatientMs.Utils
{
    public static class PatientUtils
    {
        public static PatientDto toDto(this Patient patient)
        {
            return new PatientDto
            {
                id = patient.id,
                adress = new AdressDto
                {
                    houseNumber = patient.Adress.HouseNumber,
                    street = patient.Adress.Street,
                    postalCode = patient.Adress.PostalCode
                },
                medicalProfile = new MedicalProfileDto
                {
                    bloodType = patient.MedicalProfile.BloodType.ToString(),
                    length = patient.MedicalProfile.Length,
                    weight = patient.MedicalProfile.Weight,
                },
                firstName = patient.FirstName,
                lastName = patient.LastName,
            };
        }

        public static Patient toPatient(this PatientDto patient)
        {
            BloodType bloodType;
            try
            {
                bloodType = (BloodType)Enum.Parse(typeof(BloodType), patient.medicalProfile.bloodType);
            }
            catch
            {
                throw new ArgumentException("Invalid blood type!");
            }

            return new Patient(
                new FirstName(patient.firstName),
                new LastName(patient.lastName),
                new MedicalProfile(
                new Length(patient.medicalProfile.length),
                new Weight(patient.medicalProfile.weight),
                bloodType
                ),
                new Adress(
                    patient.adress.street,
                    patient.adress.houseNumber,
                    patient.adress.postalCode
                    ));
        }


    }
}
