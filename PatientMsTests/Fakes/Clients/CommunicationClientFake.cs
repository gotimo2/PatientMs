using PatientMs.Controller.Dto.TreatmentDto;
using PatientMs.Domain;
using PatientMs.Service.Client;

namespace PatientMsTests.Fakes.Clients
{
    internal class CommunicationClientFake : ICommunicationClient
    {
        public Task SendBill(Patient patient, TreatmentDto treatment, decimal totalCost, decimal finalCost, InsurancePolicy policy)
        {
            Console.WriteLine($"Bill sent to patient {patient.FirstName} {patient.LastName} for treatment {treatment.name} with total cost {totalCost}/ final cost {finalCost} and insurancepolicy {policy.Name}");
            return Task.CompletedTask;
        }
    }
}
