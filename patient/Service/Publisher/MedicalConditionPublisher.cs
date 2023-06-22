using PatientMs.Service.Publisher.Dto;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PatientMs.Service.Publisher
{
    public interface IMedicalConditionPublisher
    {
        public Task PublishMedicalConditionAdded(long patientId, Guid conditionId);
    }

    public class MedicalConditionPublisher : IMedicalConditionPublisher
    {
        private readonly ConnectionFactory connectionFactory;

        public MedicalConditionPublisher(ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task PublishMedicalConditionAdded(long patientId, Guid conditionId)
        {
            using var channel = connectionFactory.CreateConnection().CreateModel();
            channel.QueueDeclare(
                queue: "medical-condition-added",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var dto = new MedicalConditionCreatedDto
            {
                conditionId = conditionId,
                patientId = patientId
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dto));

            channel.BasicPublish(exchange: string.Empty, "medical-condition-added", basicProperties: null, body: body);
        }
    }
}
