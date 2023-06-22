using PatientMs.Service.Publisher;

namespace PatientMsTests.Fakes.Publishers
{
    internal class MedicalConditionPublisherFake : IMedicalConditionPublisher
    {
        public MedicalConditionPublisherFake() { }

        public Task PublishMedicalConditionAdded(long patientId, Guid conditionId)
        {
            Console.WriteLine($"published medical condition {conditionId} to patient {patientId}");
            return Task.CompletedTask;
        }
    }
}
