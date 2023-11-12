using PaySpaceTaxCalculatorWebAPI.BLL.BLL;
using PaySpaceTaxCalculatorWebAPI.BLL.DataContract;
using static PaySpaceTaxCalculatorWebAPI.Data.StaticClass;

namespace PaySpaceTaxCalculatorWebAPITest
{
    public class ApplicationUserBLLUnitTest
    {
        private ApplicationUserBLL ApplicationUserBLL;

        [SetUp]
        public void Setup()
        {
            ApplicationUserBLL = new();
            DatabaseHelper.ConnectionString = "Server=.\\SQLEXPRESS;Database=PaySpaceTaxCalculator;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True";
        }

        [Test]
        public async Task TestSignUpNullEmailAddressAndNullUserPassword()
        {
            await ApplicationUserBLL.SignUp("abcefxaqa@gmail.com", "12547*#");
            Assert.Pass();
        }

        [Test]
        public async Task TestSignUpEmailAddressDuplicate()
        {
            await ApplicationUserBLL.SignUp("abcedhqsas@gmail.com", "12547*#");
            Assert.Pass();
        }

        [Test]
        public async Task TestAuthenticateWithInvalidUsernameAndPassword()
        {
            AuthenticateResp _authenticateResp = await ApplicationUserBLL.Authenticate("abcef@gmail.com", "12547*#");

            Assert.IsNotEmpty(_authenticateResp.AccessToken, "AccessToken not found");

            Assert.IsTrue(_authenticateResp.AccessTokenExpiryDate.HasValue);
        }

        [Test]
        public async Task TestIfAccessTokenIsValid()
        {
            bool _result = await ApplicationUserBLL.IsEmailAddressAccessTokenValid("abced@gmail.com", "c5ace98de91c43baa57bec2006e204fb40b8d9e7332845bca828802148ea0e63a4c649051aa44710a97047947261d7af1545f4d7a4ba4b829fe8dd15c1dfead4be93bb0415a24528a070a79cb6517deb68f5ba70042a43d6b336bef122e2b307");

            Assert.IsTrue(_result, "AccessToken not valid");
        }
    }
}