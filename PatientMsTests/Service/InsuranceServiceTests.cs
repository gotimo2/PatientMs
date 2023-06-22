using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientMs.Service;
using PatientMsTests.Fakes.Data;

namespace PatientMsTests.Service
{
    [TestClass]
    public class InsuranceServiceTests
    {

        private IInsuranceService service;

        [TestInitialize]
        public void Initialize()
        {
            service = new InsuranceService(new PatientAccessorFake());
        }


    }
}
