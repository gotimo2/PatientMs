using PatientMs.Data;
using PatientMs.Domain;
using PatientMs.Service.Client;
using PatientMs.Service.Publisher;
using PatientMs.Value;

namespace PatientMs.Service
{

    public interface IMedicalConditionService
    {
        public Task<List<MedicalCondition>> GetMedicalConditions(long id);
        public Task<List<MedicalCondition>> AddMedicalCondition(long id, MedicalCondition medicalCondition);
        public Task<List<MedicalCondition>> RemoveMedicalConditon(long id, MedicalCondition medicalCondition);
        public Task<Bill> FinalizeTreatment(long id, Guid conditionId, Guid treatmentId);
    }

    public class MedicalConditionService : IMedicalConditionService
    {

        private IPatientAccessor accessor { get; init; }
        private ITreatmentClient treatmentClient { get; init; }
        private ICommunicationClient communicationClient { get; init; }
        private IMedicalConditionPublisher publisher { get; init; }

        public MedicalConditionService(IPatientAccessor dbAccessor, ITreatmentClient treatmentClient, ICommunicationClient communicationClient, IMedicalConditionPublisher publisher)
        {
            this.publisher = publisher;
            this.accessor = dbAccessor;
            this.treatmentClient = treatmentClient;
            this.communicationClient = communicationClient;
        }


        public async Task<List<MedicalCondition>> GetMedicalConditions(long id)
        {
            var dbPatient = await accessor.GetPatient(id);
            return dbPatient.MedicalConditions;
        }

        public async Task<List<MedicalCondition>> AddMedicalCondition(long id, MedicalCondition medicalCondition)
        {
            var dbPatient = await accessor.GetPatient(id);
            if (dbPatient.MedicalConditions.Contains(medicalCondition))
            {
                throw new ArgumentException("Patient already has condition!");
            }

            if (!await treatmentClient.ConditionExists(medicalCondition.foreignId))
            {
                throw new KeyNotFoundException($"Medical condition {medicalCondition.foreignId} not found");
            }


            dbPatient.AddMedicalCondition(medicalCondition);
            await accessor.UpdatePatient(dbPatient);
            await publisher.PublishMedicalConditionAdded(id, medicalCondition.foreignId);

            return dbPatient.MedicalConditions;
        }

        public async Task<List<MedicalCondition>> RemoveMedicalConditon(long id, MedicalCondition medicalCondition)
        {
            var dbPatient = await accessor.GetPatient(id);
            dbPatient.RemoveMedicalCondition(medicalCondition);
            await accessor.UpdatePatient(dbPatient);
            return dbPatient.MedicalConditions;
        }

        public async Task<Bill> FinalizeTreatment(long patientId, Guid conditionId, Guid treatmentId)
        {
            var condition = new MedicalCondition { foreignId = conditionId };
            var dbPatient = await accessor.GetPatient(patientId);
            if (dbPatient.MedicalConditions.Contains(condition) == false)
            {
                throw new ArgumentException($"Condition with id {conditionId} not found on patient.");
            }
            var treatment = await treatmentClient.GetTreatment(treatmentId);

            decimal totalCost = treatment.totalPrice;
            decimal cost = treatment.totalPrice;
            InsurancePolicy mostEffectivePolicy = InsurancePolicy.NoInsurance();
            foreach (var policy in dbPatient.InsurancePolicies)
            {
                if (policy.apply(cost) < mostEffectivePolicy.apply(cost))
                {
                    mostEffectivePolicy = policy;
                }
            }

            cost = mostEffectivePolicy.apply(cost);

            await communicationClient.SendBill(dbPatient, treatment, totalCost, cost, mostEffectivePolicy);

            Bill bill = new Bill
            {
                condition = condition,
                cost = new Cost(cost, treatment.name),
                appliedInsurance = mostEffectivePolicy
            };

            dbPatient.RemoveMedicalCondition(condition);

            await accessor.UpdatePatient(dbPatient);

            return bill;
        }


    }
}
