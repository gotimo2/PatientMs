using PatientMs.Data;
using PatientMs.Domain;

namespace PatientMs.Service
{

    public interface IPatientService
    {
        public Task<Patient> GetPatient(long id);
        public Task<List<Patient>> GetByName(string firstName, string lastName);
        public Task<Patient> NewPatient(Patient patient);
        public Task<Patient> UpdatePatient(long id, Patient patient);
        public Task DeletePatient(long id);
    }


    public class PatientService : IPatientService
    {
        private IPatientAccessor accessor { get; init; }
        public PatientService(IPatientAccessor accessor)
        {
            this.accessor = accessor;
        }

        public async Task<Patient> GetPatient(long id)
        {
            return await accessor.GetPatient(id);
        }


        public async Task<List<Patient>> GetByName(string firstName, string lastName)
        {
            return await accessor.GetByName(firstName, lastName);
        }

        public async Task<Patient> NewPatient(Patient patient)
        {

            await accessor.AddPatient(patient);
            return patient;

        }

        public async Task DeletePatient(long id)
        {
            var patient = await GetPatient(id);
            await accessor.DeletePatient(patient);
        }

        public async Task<Patient> UpdatePatient(long id, Patient patient)
        {
            var dbPatient = await GetPatient(id);
            dbPatient.CopyValidAttributes(patient);
            await accessor.UpdatePatient(dbPatient);
            return dbPatient;
        }

    }
}
