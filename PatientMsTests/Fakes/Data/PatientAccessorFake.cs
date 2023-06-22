using PatientMs.Data;
using PatientMs.Domain;
using PatientMs.Value;
using PatientMs.Value.Insurance;
using PatientMs.Value.MedicalDetails;

namespace PatientMsTests.Fakes.Data
{
    internal class PatientAccessorFake : IPatientAccessor
    {
        private Patient jan()
        {
            var jan = new Patient
            (
                new FirstName("Jan"),
                new LastName("Janssen"),
                new MedicalProfile(new Length(1.872), new Weight(72.0), BloodType.AB),
                new Adress("Panstraat", "17A", "1102LI")
            );

            jan.AddMedicalCondition(new MedicalCondition { foreignId = Guid.Parse("e5b1e9f7-86fb-408d-85b5-ca80951db7a2") });
            jan.AddInsurancePolicy(new InsurancePolicy("Basisverzekering", new Coverage(50), new Deductible(400)));

            return jan;
        }

        async Task IPatientAccessor.AddPatient(Patient patient)
        {
            Console.WriteLine($"patient {patient.FirstName} {patient.LastName} added");
            await Task.CompletedTask;
        }

        async Task IPatientAccessor.DeletePatient(Patient patient)
        {
            if (patient.id == 1)
            {
                await Task.CompletedTask;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        async Task<List<Patient>> IPatientAccessor.GetByName(string firstName, string lastName)
        {
            if (firstName == "Jan" && lastName == "Janssen")
            {
                return new List<Patient> { jan() };
            }
            else
            {
                return new List<Patient>();
            }
        }

        async Task<Patient> IPatientAccessor.GetPatient(long id)
        {
            if (id == 1)
            {
                return jan();
            }
            else
            {
                throw new KeyNotFoundException($"patient with id {id} not found");
            }
        }

        async Task IPatientAccessor.UpdatePatient(Patient patient)
        {
            Console.WriteLine($"patient {patient.FirstName} {patient.LastName} updated");
            await Task.CompletedTask;
        }
    }
}
