using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientMs.Domain;
using PatientMs.Value;
using PatientMs.Value.MedicalDetails;

namespace PatientMsTests.Data
{
    [TestClass]
    public class ConnectionTest
    {
        [TestMethod("Can establish connection to and use database using database scaffolding")]
        public void DatabaseInitializeTest()
        {
            try
            {
                var store = TestScaffolding.GetDocumentStore();
                var session = store.OpenSession();
                session.Insert(new Patient
                    (
                        new FirstName("Jen"),
                        new LastName("Jenssen"),
                        new MedicalProfile(new Length(1.82), new Weight(73.0), BloodType.O),
                        new Adress("Penstraat", "19B", "1103KB")
                    ));
                session.SaveChanges();
                var p = store!.OpenSession().Query<Patient>().Where(p => p.FirstName == "Jen" && p.LastName == "Jenssen").FirstOrDefault();
                store.Advanced.Clean.DeleteAllDocuments();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
    }
}
