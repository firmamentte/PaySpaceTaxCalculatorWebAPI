using PaySpaceTaxCalculatorWebAPI.BLL.BLL;
using PaySpaceTaxCalculatorWebAPI.BLL.DataContract;
using static PaySpaceTaxCalculatorWebAPI.Data.StaticClass;

namespace PaySpaceTaxCalculatorWebAPITest
{
    public class PostalCodeBLLUnitTest
    {
        private PostalCodeBLL PostalCodeBLL;

        [SetUp]
        public void Setup()
        {
            PostalCodeBLL = new();
            DatabaseHelper.ConnectionString = "Server=.\\SQLEXPRESS;Database=PaySpaceTaxCalculator;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True";
        }

        [Test]
        public async Task TestGetPostalCodes()
        {
            List<PostalCodeResp> _postalCodeResps = await PostalCodeBLL.GetPostalCodes();
            Assert.IsTrue(_postalCodeResps.Count == 4, "Invalid Postal Code Count");
        }
    }
}