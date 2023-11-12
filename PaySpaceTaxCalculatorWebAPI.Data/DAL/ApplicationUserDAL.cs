using Microsoft.EntityFrameworkCore;
using PaySpaceTaxCalculatorWebAPI.Data.Entities;

namespace PaySpaceTaxCalculatorWebAPI.Data.DAL
{
    public class ApplicationUserDAL
    {
        public async Task<ApplicationUser?> GetApplicationUserByUsername(PaySpaceTaxCalculatorContext dbContext, string emailAddress)
        {
            return await (from applicationUser in dbContext.ApplicationUsers
                          where applicationUser.Username == emailAddress
                          select applicationUser).
                          FirstOrDefaultAsync();
        }

        public async Task<bool> IsUsernameExisting(PaySpaceTaxCalculatorContext dbContext, string emailAddress)
        {
            return await (from applicationUser in dbContext.ApplicationUsers
                          where applicationUser.Username == emailAddress
                          select applicationUser).
                          AnyAsync();
        }
    }
}
