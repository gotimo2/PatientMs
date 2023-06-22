using PatientMs.Controller.Dto;
using PatientMs.Controller.Dto.TreatmentDto;
using PatientMs.Domain;

namespace PatientMs.Service.Client
{

    public interface ICommunicationClient
    {
        public Task SendBill(Patient patient, TreatmentDto treatment, decimal totalCost, decimal finalCost, InsurancePolicy policy);
    }

    public class CommunicationClient : ICommunicationClient
    {
        private IHttpClientFactory httpClientFactory;
        public CommunicationClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task SendBill(Patient patient, TreatmentDto treatment, decimal totalCost, decimal finalCost, InsurancePolicy policy)
        {

            var client = httpClientFactory.CreateClient("communication");


            string emailContent =
            $"Dear {patient.FirstName} {patient.LastName}, Here is the bill for your treatment for {treatment.name}. \n" +
            $"the total cost is €{totalCost}."
            + $"\n your insurance policy of  \"{policy.Name}\" applies, bringing your final cost to {finalCost}. "
            + "\n\nThank you for choosing FIKT medical services under HUMC.";

            var communication = new CommunicationDto
            {
                medium = "EMAIL",
                patientId = patient.id,
                description = "Bill for your treatment",

                content = emailContent,

                scheduledDate = DateTime.UtcNow.AddMinutes(5),

                reminder = new CommunicationReminderDto
                {
                    amount = 2,
                    deadline = DateTime.UtcNow.AddDays(14)
                }

            };
            var communicationClient1 = httpClientFactory.CreateClient("communication");
            await client.PostAsJsonAsync("/messages", communication);
        }

    }
}
