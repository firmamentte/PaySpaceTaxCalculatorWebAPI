using PaySpaceTaxCalculatorWebAPI.BLL.BLL;
using PaySpaceTaxCalculatorWebAPI.BLL.DataContract;
using static PaySpaceTaxCalculatorWebAPI.Data.StaticClass;

namespace PaySpaceTaxCalculatorWebAPITest
{
    public class TaxCalculationBLLUnitTest
    {
        private TaxCalculationBLL TaxCalculationBLL;

        [SetUp]
        public void Setup()
        {
            TaxCalculationBLL = new();
            DatabaseHelper.ConnectionString = "Server=.\\SQLEXPRESS;Database=PaySpaceTaxCalculator;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True";
        }

        [Test]
        public async Task TestGetTaxCalculationResultsByEmailAddress()
        {
            List<AnnualIncomeTaxResp> _annualIncomeTaxResps = await TaxCalculationBLL.GetTaxCalculationsByEmailAddress("abced@gmail.com");
            Assert.IsTrue(_annualIncomeTaxResps.Count == 25, "Invalid Annual Income Tax");
        }

        [Test]
        public async Task TestCreateAnnualIncomeTaxWithInvalidPostalCode()
        {
            await TaxCalculationBLL.CreateAnnualIncomeTax("abced@gmail.com", new AnnualIncomeTaxReq()
            {
                AnnualIncome = 0,
                PostalCode = "7441"
            });
            Assert.Pass();
        }

        [Test]
        public async Task TestCreateAnnualIncomeTaxForAnnualIncomeTaxEqual1252_65()
        {
            AnnualIncomeTaxResp annualIncomeTaxResp = await TaxCalculationBLL.CreateAnnualIncomeTax("abced@gmail.com", new AnnualIncomeTaxReq()
            {
                AnnualIncome = 8351M,
                PostalCode = "7441"
            });

            Assert.That(annualIncomeTaxResp.AnnualIncomeTax, Is.EqualTo(1252.65M), "Invalid Annual Income Tax");
        }

        [Test]
        public async Task TestCreateAnnualIncomeTaxWithForAnnualIncomeTaxForFlatValue()
        {
            AnnualIncomeTaxResp annualIncomeTaxResp = await TaxCalculationBLL.CreateAnnualIncomeTax("abced@gmail.com", new AnnualIncomeTaxReq()
            {
                AnnualIncome = 200000M,
                PostalCode = "A100"
            });

            Assert.IsTrue(annualIncomeTaxResp.AnnualIncomeTax == 10000M, "Invalid Annual Income Tax");
        }

        [Test]
        public async Task TestCreateAnnualIncomeTaxWithForAnnualIncomeTaxForFlatRate()
        {
            AnnualIncomeTaxResp annualIncomeTaxResp = await TaxCalculationBLL.CreateAnnualIncomeTax("abced@gmail.com", new AnnualIncomeTaxReq()
            {
                AnnualIncome = 500000,
                PostalCode = "7000"
            });

            Assert.IsTrue(annualIncomeTaxResp.AnnualIncomeTax == 87500, "Invalid Annual Income Tax");
        }
    }
}