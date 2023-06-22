using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientMs.Domain;
using PatientMs.Service;
using PatientMsTests.Fakes.Clients;
using PatientMsTests.Fakes.Data;
using PatientMsTests.Fakes.Publishers;

namespace PatientMsTests.Service
{
    [TestClass]
    public class MedicalConditionServiceTests
    {
        private const string NotOnPatientId = "f53243e4-d06c-42a8-8f28-838ffa4e559b";
        private const string AlreadyOnPatientId = "e5b1e9f7-86fb-408d-85b5-ca80951db7a2";
        private const string NonexistentConditionId = "055ef622-e6d1-4f1e-825e-904ea48c868f";

        private const string TreatmentId = "a24e781f-e221-4e8a-84f2-8de8b810d99a";

        private IMedicalConditionService _service;

        [TestInitialize]
        public void setup()
        {
            _service = new MedicalConditionService(new PatientAccessorFake(), new TreatmentClientFake(),
                    new CommunicationClientFake(),
                    new MedicalConditionPublisherFake()
                );
        }


        [TestMethod("Test adding a condition to a patient")]
        public void TestAddingCondition()
        {
            var conditions = _service.AddMedicalCondition(1, new MedicalCondition { foreignId = Guid.Parse(NotOnPatientId) }).Result;

            Assert.IsTrue(conditions.Contains(new MedicalCondition { foreignId = Guid.Parse(NotOnPatientId) }));
        }

        [TestMethod("adding a duplicate condition leads to an error")]
        public async Task TestAddingDuplicateCondition()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await _service.AddMedicalCondition(1, new MedicalCondition { foreignId = Guid.Parse(AlreadyOnPatientId) });
            });
        }

        [TestMethod("adding a nonexistent condition leads to an error")]
        public async Task TestAddingNonexistentCondition()
        {
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () =>
            {
                await _service.AddMedicalCondition(1, new MedicalCondition { foreignId = Guid.Parse(NonexistentConditionId) });
            });
        }

        [TestMethod("Finalizing a treatment returns a bill with the correct price")]
        public void FinalizeTreatmentTest()
        {
            var result = _service.FinalizeTreatment(1, Guid.Parse(AlreadyOnPatientId), Guid.Parse(TreatmentId)).Result;
            Assert.AreEqual(result.cost.Value, 200);
        }

        [TestMethod("Finalizing a treatment on a condition a patient doesn't have throws an error")]
        public async Task IncorrectFinalizationTest()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            {
                await _service.FinalizeTreatment(1, Guid.Parse(NotOnPatientId), Guid.Parse(TreatmentId));
            });
        }
    }
}
