using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientMs.Value;
using PatientMs.Value.Insurance;
using PatientMs.Value.MedicalDetails;

namespace PatientMsTests.Value
{
    [TestClass]
    public class ValueTests
    {
        #region Adress tests

        [TestMethod("Adress constructor throws error with invalid postal code")]
        public void InvalidPostalCodeTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Adress("panstraat", "18", "1400002AB");
            });
        }

        [TestMethod("Adress constructor throws error with empty street name")]
        public void InvalidStreetTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Adress("", "18", "1402RD");
            });
        }

        [TestMethod("Adress constructor throws error with empty house number")]
        public void InvalidHouseNumberTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Adress("panstraat", "", "1402RD");
            });
        }


        [TestMethod("Adress constructor works with correct information")]
        public void ValidPostalCodeTest()
        {
            try
            {
                new Adress("panstraat", "18D", "1402RD");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        #endregion

        #region Cost tests

        [TestMethod("Cost throws an error when negative")]
        public void InvalidCostTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Cost(-30, "impossible product");
            });
        }

        #endregion

        #region Weight tests

        [TestMethod("Weight throws an error when too large")]
        public void HighInvalidWeightTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Weight(2390);
            });
        }

        [TestMethod("Weight throws an error when too low")]
        public void LowInvalidWeightTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Weight(-1);
            });
        }

        #endregion

        #region Length tests

        [TestMethod("Length throws an error when too large")]
        public void HighInvalidLengthTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Length(2390);
            });
        }

        [TestMethod("Length throws an error when too low")]
        public void LowInvalidLengthTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Length(-1);
            });
        }

        #endregion

        #region Coverage tests

        [TestMethod("Coverage cannot be above 100")]
        public void HighInvalidCoverageTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Coverage(101);
            });
        }

        [TestMethod("Coverage cannot be below 0")]
        public void LowInvalidCoverageTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Coverage(-1);
            });
        }

        #endregion

        #region Deductible tests

        [TestMethod("Deductible cannot be above 885")]
        public void HighInvalidDeductibleTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Deductible(1000);
            });
        }

        [TestMethod("Deductible cannot be below 385")]
        public void LowInvalidDeductibleTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Deductible(300);
            });
        }

        #endregion

        #region Name tests

        [TestMethod("FirstName cannot be empty")]
        public void EmptyFirstNameTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new FirstName("");
            });
        }

        [TestMethod("LastName cannot be Empty")]
        public void EmptyLastNameTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new LastName("");
            });
        }

        #endregion

    }
}
