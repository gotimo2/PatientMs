using PatientMs.Consumer.Dto;
using PatientMs.Service;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using PatientMs.Utils;
using RabbitMQ.Client.Events;

namespace PatientMs.Consumer
{
    public class InsuranceConsumer : MessageConsumer
    {

        private IInsuranceService service;

        public InsuranceConsumer(ConnectionFactory connectionFactory, IInsuranceService service) : base(connectionFactory)
        {
            this.service = service;
        }

        public override void Initialize()
        {
            registerQueueListener("add-insurance-policy", (object? _, BasicDeliverEventArgs eventargs) =>
            {
                Console.WriteLine("Add insurance policy received");
                try
                {
                    var dto = JsonSerializer.Deserialize<AddInsurancePolicyDto>(Encoding.UTF8.GetString(eventargs.Body.ToArray()));
                    service.AddInsurancePolicy(dto.patientId, dto.ToInsurancePolicy());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            });
        }
    }
}
