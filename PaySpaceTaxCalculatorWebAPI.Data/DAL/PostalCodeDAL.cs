using Microsoft.EntityFrameworkCore;
using PaySpaceTaxCalculatorWebAPI.Data.Entities;

namespace PaySpaceTaxCalculatorWebAPI.Data.DAL
{
    public class PostalCodeDAL
    {
        public async Task<List<PostalCode>> GetPostalCodes(PaySpaceTaxCalculatorContext dbContext)
        {
            return await (from postalCode in dbContext.PostalCodes
                          select postalCode).
                          ToListAsync();
        }
    }
}
