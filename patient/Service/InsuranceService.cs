using PatientMs.Data;
using PatientMs.Domain;

namespace PatientMs.Service
{
    public interface IInsuranceService
    {
        public Task<List<InsurancePolicy>> AddInsurancePolicy(long id, InsurancePolicy insurance);
        public Task<List<InsurancePolicy>> GetInsurancePolicies(long id);
    }



    public class InsuranceService : IInsuranceService
    {
        private IPatientAccessor accessor { get; init; }


        public InsuranceService(IPatientAccessor dbAccessor)
        {
            accessor = dbAccessor;
        }


        public async Task<List<InsurancePolicy>> GetInsurancePolicies(long id)
        {
            var dbPatient = await accessor.GetPatient(id);
            return dbPatient.InsurancePolicies;
        }

        public async Task<List<InsurancePolicy>> AddInsurancePolicy(long id, InsurancePolicy insurance)
        {
            var dbPatient = await accessor.GetPatient(id);
            dbPatient.AddInsurancePolicy(insurance);
            await accessor.UpdatePatient(dbPatient);
            return dbPatient.InsurancePolicies;
        }


    }
}
