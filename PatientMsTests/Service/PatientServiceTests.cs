using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientMsTests.Fakes.Data;

namespace PatientMs.Service.Tests
{

    [TestClass()]
    public class PatientServiceTests
    {

        private IPatientService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new PatientService(new PatientAccessorFake());
        }

        [TestMethod("Updating a patient returns the updated patient")]
        public void PatientServiceTest()
        {
            var patient = _service.GetPatient(1).Result;

            patient.FirstName = "Test";
            patient.LastName = "Test";
            patient.Adress.Street = "Teststraat";
            patient.MedicalProfile.Weight = 89.1;

            var updatedPatient = _service.UpdatePatient(1, patient).Result;

            Assert.AreEqual(updatedPatient.FirstName, patient.FirstName);
            Assert.AreEqual(updatedPatient.LastName, patient.LastName);
            Assert.AreEqual(updatedPatient.Adress.Street, "Teststraat");
            Assert.AreEqual(updatedPatient.MedicalProfile.Weight, 89.1);
        }
    }
}