using Microsoft.EntityFrameworkCore;
using PaySpaceTaxCalculatorWebAPI.Data.DataContract;
using PaySpaceTaxCalculatorWebAPI.Data.Entities;

namespace PaySpaceTaxCalculatorWebAPI.Data.DAL
{
    public class TaxCalculationResultDAL
    {
        public async Task<PostalCodeTaxCalculationTypeIncomeRangeTaxRate> GetPostalCodeTaxCalculationTypeIncomeRangeTaxRate(
        PaySpaceTaxCalculatorContext dbContext, string postalCodeValue, decimal annualIncome)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await (from postalCode in dbContext.PostalCodes

                          join postalCodeTaxCalculationType in dbContext.PostalCodeTaxCalculationTypes
                          on postalCode.PostalCodeId equals postalCodeTaxCalculationType.PostalCodeId

                          join taxCalculationType in dbContext.TaxCalculationTypes
                          on postalCodeTaxCalculationType.TaxCalculationTypeId equals taxCalculationType.TaxCalculationTypeId

                          join taxCalculationTypeIncomeRange in dbContext.TaxCalculationTypeIncomeRanges
                          on taxCalculationType.TaxCalculationTypeId equals taxCalculationTypeIncomeRange.TaxCalculationTypeId

                          join incomeRange in dbContext.IncomeRanges
                          on taxCalculationTypeIncomeRange.IncomeRangeId equals incomeRange.IncomeRangeId

                          join incomeRangeTaxRate in dbContext.IncomeRangeTaxRates
                          on incomeRange.IncomeRangeId equals incomeRangeTaxRate.IncomeRangeId

                          join taxRate in dbContext.TaxRates
                          on incomeRangeTaxRate.TaxRateId equals taxRate.TaxRateId

                          where postalCode.PostalCodeValue == postalCodeValue &&
                                annualIncome >= incomeRange.IncomeRangeFrom &&
                                annualIncome <= incomeRange.IncomeRangeTo
                          select new PostalCodeTaxCalculationTypeIncomeRangeTaxRate
                          {
                              PostalCode = postalCode,
                              TaxCalculationType = taxCalculationType,
                              IncomeRange = incomeRange,
                              TaxRate = taxRate,
                          }).
                          FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<TaxCalculationResult>> GetTaxCalculationResultsByEmailAddress(PaySpaceTaxCalculatorContext dbContext, string emailAddress)
        {
            return await (from taxCalculationResult in dbContext.TaxCalculationResults

                          join applicationUser in dbContext.ApplicationUsers
                          on taxCalculationResult.ApplicationUserId equals applicationUser.ApplicationUserId

                          where applicationUser.Username == emailAddress
                          select taxCalculationResult).
                          ToListAsync();
        }
    }
}
