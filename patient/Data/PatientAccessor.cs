using Marten;
using Marten.Exceptions;
using PatientMs.Domain;
using System.Text.Json;

namespace PatientMs.Data
{
    public interface IPatientAccessor
    {
        public Task<Patient> GetPatient(long id);
        public Task<List<Patient>> GetByName(string firstName, string lastName);
        public Task AddPatient(Patient patient);
        public Task UpdatePatient(Patient patient);
        public Task DeletePatient(Patient patient);

    }


    #region Marten-based PatientAccessor

    public class MartenPatientAccessor : IPatientAccessor
    {

        private IDocumentStore store { get; }

        public MartenPatientAccessor(IDocumentStore store)
        {
            this.store = store;
        }

        public async Task AddPatient(Patient patient)
        {
            await using (var session = store.LightweightSession())
            {
                session.Store(patient);
                await session.SaveChangesAsync();
            }
        }

        public async Task DeletePatient(Patient patient)
        {
            await using (var session = store.OpenSession())
            {
                session.Delete(patient);
                await session.SaveChangesAsync();
            }
        }

        public async Task<List<Patient>> GetByName(string firstName, string lastName)
        {
            await using (var session = store.QuerySession())
            {
                var patients = session.Query<Patient>().Where(p => p.FirstName == firstName && p.LastName == lastName).ToList();
                return patients;
            }
        }

        public async Task<Patient> GetPatient(long id)
        {
            await using (var session = store.QuerySession())
            {
                    var patient = session.Query<Patient>().Where(p => p.id == id)
                        .FirstOrDefault();
                    return patient ?? throw new KeyNotFoundException($"no patient with id {id} found");
            }

        }

        public async Task UpdatePatient(Patient patient)
        {
            await using (var session = store.OpenSession())
            {
                Console.WriteLine("Updating patient");
                Console.WriteLine("Patient JSON to serialize: \n" + JsonSerializer.Serialize(patient));
                session.Store(patient.MedicalConditions.ToArray());
                session.Store(patient.InsurancePolicies.ToArray());
                session.Store(patient);
                await session.SaveChangesAsync();
            }
        }
    }

    #endregion
}
