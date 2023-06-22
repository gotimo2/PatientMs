using Marten;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientMs.Data;
using PatientMs.Domain;
using PatientMs.Value;
using PatientMs.Value.Insurance;
using PatientMs.Value.MedicalDetails;
using System.Text.Json;

namespace PatientMsTests.Data
{
    [TestClass]
    public class DataTests
    {

        #region setup/teardown

        private DocumentStore documentStore;
        private IPatientAccessor patientAccessor;

        [TestInitialize()]
        public void PopulateDatabase()
        {
            documentStore = TestScaffolding.GetDocumentStore();
            patientAccessor = new MartenPatientAccessor(documentStore);
            var session = documentStore.OpenSession();
            session.Insert(new Patient
            (
            new FirstName("Jen"),
            new LastName("Jenssen"),
            new MedicalProfile(new Length(1.782), new Weight(73.0), BloodType.O),
            new Adress("Penstraat", "19B", "1103KB")
            ));
            session.SaveChanges();
        }

        [TestCleanup]
        public void CleanupDatabase()
        {
            documentStore.Advanced.Clean.DeleteAllDocuments();
        }

        #endregion

        #region ORM tests

        [TestMethod("can insert data using ORM")]
        public void CanInsertData()
        {
            var session = documentStore.LightweightSession();
            session.Insert(new Patient
            (
            new FirstName("Jan"),
            new LastName("Janssen"),
            new MedicalProfile(new Length(1.82), new Weight(72.0), BloodType.AB),
            new Adress("Panstraat", "17A", "1102LI")
            ));
            session.SaveChanges();
            Assert.AreEqual(2, documentStore.QuerySession().Query<Patient>().Count());
        }

        [TestMethod("Can get patient by name using ORM")]
        public void CanGetPatient()
        {
            var result = documentStore.OpenSession().Query<Patient>().Where(patient => patient.FirstName == "Jen" && patient.LastName == "Jenssen").Count();
            Assert.AreEqual(1, result);
        }

        [TestMethod("Can update patient using ORM")]
        public void CanUpdatePatient()
        {

            var p = documentStore.OpenSession().Query<Patient>().Where(patient => patient.FirstName == "Jen" && patient.LastName == "Jenssen").First();
            p.FirstName = "Jan";
            p.LastName = "Janssen";
            var session = documentStore.LightweightSession();
            session.Update(p);
            session.SaveChanges();
            var result = session.Query<Patient>().Where(patient => patient.FirstName == "Jan" && patient.LastName == "Janssen").Count();
            Assert.AreEqual(1, result);
        }

        [TestMethod("Can delete patient using ORM")]
        public void CanDeletePatient()
        {
            var p = documentStore.OpenSession().Query<Patient>().Where(patient => patient.FirstName == "Jen" && patient.LastName == "Jenssen").First();
            var session = documentStore.LightweightSession();
            session.Delete(p);
            session.SaveChanges();
            var count = documentStore.OpenSession().Query<Patient>().Where(patient => patient.FirstName == "Jen" && patient.LastName == "Jenssen").Count();
            Assert.AreEqual(0, count);
        }

        #endregion

        #region Accessor tests

        [TestMethod("Accessor can get patient by name")]
        public void AccessorCanGetPatientByName()
        {
            var result = patientAccessor.GetByName("Jen", "Jenssen").Result;
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod("Accessor can get patient by id")]
        public void AccessorCanGetPatientById()
        {
            var patientFromName = patientAccessor.GetByName("Jen", "Jenssen").Result[0];
            try
            {
                var patientFromId = patientAccessor.GetPatient(patientFromName.id);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod("Accessor can add patient")]
        public void AccessorCanAddPatient()
        {
            patientAccessor.AddPatient(new Patient
            (
            new FirstName("Jan"),
            new LastName("Janssen"),
            new MedicalProfile(new Length(2.2), new Weight(72.0), BloodType.AB),
            new Adress("Panstraat", "17A", "1102LI")
            ));

            var list = patientAccessor.GetByName("Jan", "Janssen").Result;

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod("Accessor can update patient")]
        public void AccessorCanUpdatePatient()
        {
            var patientFromName = patientAccessor.GetByName("Jen", "Jenssen").Result[0];
            patientFromName.FirstName = "Jan";
            patientFromName.LastName = "Janssen";
            patientFromName.AddInsurancePolicy(new InsurancePolicy("BasisPolicy", new Coverage((decimal)40.0), new Deductible(500)));
            patientAccessor.UpdatePatient(patientFromName).Wait();
            var patientFromId = patientAccessor.GetPatient(patientFromName.id).Result;
            Console.WriteLine("deserialized: " + JsonSerializer.Serialize(patientFromId));
            Assert.IsTrue(patientFromId.InsurancePolicies.Count > 0);
        }

        [TestMethod("Accessor throws an error when a patient cannot be found")]
        public async Task AccessorCanThrow()
        {
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () =>
            {
                await patientAccessor.GetPatient(498249);
            });
        }
        #endregion

    }
}
