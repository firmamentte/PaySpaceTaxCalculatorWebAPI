using PaySpaceTaxCalculatorWebAPI.BLL.DataContract;
using PaySpaceTaxCalculatorWebAPI.Data.DAL;
using PaySpaceTaxCalculatorWebAPI.Data.Entities;

namespace PaySpaceTaxCalculatorWebAPI.BLL.BLL
{
    public class ApplicationUserBLL
    {
        private readonly ApplicationUserDAL ApplicationUserDAL;

        public ApplicationUserBLL()
        {
            ApplicationUserDAL = new();
        }

        public async Task SignUp(string emailAddress, string userPassword)
        {
            using PaySpaceTaxCalculatorContext _dbContext = new();

            if (await ApplicationUserDAL.IsUsernameExisting(_dbContext, emailAddress))
            {
                throw new Exception("EmailAddress already existing");
            }

            await _dbContext.AddAsync(
            new ApplicationUser()
            {
                ApplicationUserId = Guid.NewGuid(),
                Username = emailAddress,
                UserPassword = FirmamentUtilities.Utilities.SecretHasherHelper.Hash(userPassword)
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task<AuthenticateResp> Authenticate(string emailAddress, string userPassword)
        {
            using PaySpaceTaxCalculatorContext _dbContext = new();

            ApplicationUser? _applicationUser = await ApplicationUserDAL.GetApplicationUserByUsername(_dbContext, emailAddress);

            if (_applicationUser is null)
            {
                throw new Exception("Invalid Username or Password");
            }

            if (!string.Equals(_applicationUser.Username, emailAddress, StringComparison.CurrentCulture))
            {
                throw new Exception("Invalid Username or Password");
            }

            if (!FirmamentUtilities.Utilities.SecretHasherHelper.Verify(userPassword, _applicationUser.UserPassword))
            {
                throw new Exception("Invalid Username or Password");
            }

            string _accessToken = CreateAccessToken();

            _applicationUser.AccessToken = FirmamentUtilities.Utilities.SecretHasherHelper.Hash(_accessToken);
            _applicationUser.AccessTokenExpiryDate = DateTime.Now.AddMonths(1).Date;

            await _dbContext.SaveChangesAsync();

            return FillAuthenticateResp(_accessToken, _applicationUser.AccessTokenExpiryDate);
        }

        public async Task<bool> IsEmailAddressAccessTokenValid(string emailAddress, string accessToken)
        {
            using PaySpaceTaxCalculatorContext _dbContext = new();

            ApplicationUser? _applicationUser = await ApplicationUserDAL.GetApplicationUserByUsername(_dbContext, emailAddress);

            if (_applicationUser is null)
            {
                return false;
            }

            if (!string.Equals(_applicationUser.Username, emailAddress, StringComparison.CurrentCulture))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(_applicationUser.AccessToken))
            {
                return false;
            }

            if (!FirmamentUtilities.Utilities.SecretHasherHelper.Verify(accessToken, _applicationUser.AccessToken))
            {
                return false;
            }

            if (_applicationUser.AccessTokenExpiryDate is null)
            {
                return false;
            }

            if (_applicationUser.AccessTokenExpiryDate < DateTime.Now.Date)
            {
                return false;
            }

            return true;
        }

        private AuthenticateResp FillAuthenticateResp(string accessToken, DateTime? accessTokenExpiryDate)
        {
            return new AuthenticateResp()
            {
                AccessToken = accessToken,
                AccessTokenExpiryDate = accessTokenExpiryDate,
            };
        }

        private string CreateAccessToken()
        {
            return $"{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}";
        }
    }
}
