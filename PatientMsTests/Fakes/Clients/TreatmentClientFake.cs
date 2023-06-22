using PatientMs.Controller.Dto.TreatmentDto;
using PatientMs.Service.Client;

namespace PatientMsTests.Fakes.Clients
{
    internal class TreatmentClientFake : ITreatmentClient
    {

        private List<Guid> ConditionGuids = new List<Guid> { Guid.Parse("f53243e4-d06c-42a8-8f28-838ffa4e559b"), Guid.Parse("e5b1e9f7-86fb-408d-85b5-ca80951db7a2") };
        private List<Guid> TreatmentGuids = new List<Guid> { Guid.Parse("a24e781f-e221-4e8a-84f2-8de8b810d99a") };

        public Task<bool> ConditionExists(Guid id)
        {
            if (ConditionGuids.Contains(id))
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }


        public Task<TreatmentDto> GetTreatment(Guid id)
        {
            if (TreatmentGuids.Contains(id))
            {
                return Task.FromResult(new TreatmentDto
                {
                    totalPrice = 920,
                    id = 1,
                    name = "Test treatment",
                    treatmentConsistsOf = new List<TreatmentProcess>
                    {
                        new TreatmentProcess {
                            type = "blood taking",
                            maximumTimeInMinutes = 5,
                            minimalTimeInMinutes = 0,
                            cost = 920,
                            id = 1,
                            isAvailable = true,
                            location = "doctorsOffice",
                        }
                    }.ToArray()
                });
            }
            throw new KeyNotFoundException();
        }

    }
}
