using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientMs;
using PatientMs.Controller.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace PatientMsTests.Mvc
{

    //these tests are set up to run in parallel to the patientMs, not instead of them.

    [TestClass]
    [TestCategory("Integration")]
    public class IntegrationTests
    {

        private HttpClient client;

        private IConfiguration config = TestScaffolding.GetConfiguration();


        [TestInitialize]
        public void TestSetup()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(Environment.GetEnvironmentVariable("PATIENTSERVICE_URL") ?? config.GetValue<string>("PatientService:Adress") ?? throw new ArgumentNullException("No adress defined for patient service!"))
            };
        }

        [TestMethod]

        public async Task NewPatient()
        {
            var result =  await client.PostAsJsonAsync("", new PatientDto
            {
                adress = new AdressDto
                {
                    houseNumber = "109",
                    street = "Panstraat",
                    postalCode = "2803LI"
                },
                firstName = "Jan",
                lastName = "Janssen",
                medicalProfile = new MedicalProfileDto
                {
                    length = 1.84,
                    bloodType = "O",
                    weight = 77.4
                }
            });


            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        
        [TestCleanup]
        public void TestCleanup()
        {
            TestScaffolding.GetDocumentStore().Advanced.Clean.DeleteAllDocuments();
        }


    }
}
