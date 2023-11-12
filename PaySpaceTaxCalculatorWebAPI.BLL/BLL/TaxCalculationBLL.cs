using PaySpaceTaxCalculatorWebAPI.BLL.DataContract;
using PaySpaceTaxCalculatorWebAPI.Data.DAL;
using PaySpaceTaxCalculatorWebAPI.Data.Entities;

namespace PaySpaceTaxCalculatorWebAPI.BLL.BLL
{
    public class TaxCalculationBLL
    {
        private readonly TaxCalculationResultDAL TaxCalculationResultDAL;
        private readonly ApplicationUserDAL ApplicationUserDAL;

        public TaxCalculationBLL()
        {
            TaxCalculationResultDAL = new();
            ApplicationUserDAL = new();
        }

        public async Task<List<AnnualIncomeTaxResp>> GetTaxCalculationsByEmailAddress(string emailAddress)
        {
            using PaySpaceTaxCalculatorContext _dbContext = new();

            List<AnnualIncomeTaxResp> _annualIncomeTaxResps = new();

            foreach (var item in await TaxCalculationResultDAL.GetTaxCalculationResultsByEmailAddress(_dbContext, emailAddress))
            {
                _annualIncomeTaxResps.Add(FillAnnualIncomeTaxResp(item));
            }

            return _annualIncomeTaxResps;
        }

        public async Task<AnnualIncomeTaxResp> CreateAnnualIncomeTax(string emailAddress, AnnualIncomeTaxReq annualIncomeTaxReq)
        {
            using PaySpaceTaxCalculatorContext _dbContext = new();

            var _result = await TaxCalculationResultDAL.GetPostalCodeTaxCalculationTypeIncomeRangeTaxRate(
            _dbContext, annualIncomeTaxReq.PostalCode, annualIncomeTaxReq.AnnualIncome);

            if (_result is null)
            {
                throw new Exception("Could not Create Annual Income Tax, bad request.");
            }

            if (_result.PostalCode is null)
            {
                throw new Exception("Postal Code Not Found. The resource has been removed, had its name changed, or is unavailable.");
            }

            if (_result.TaxCalculationType is null)
            {
                throw new Exception("Tax Calculation Type Not Found. The resource has been removed, had its name changed, or is unavailable.");
            }

            if (_result.IncomeRange is null)
            {
                throw new Exception("Income Range Not Found. The resource has been removed, had its name changed, or is unavailable.");
            }

            if (_result.TaxRate is null)
            {
                throw new Exception("Tax Rate Not Found. The resource has been removed, had its name changed, or is unavailable.");
            }

            var _taxCalculationResult = new TaxCalculationResult()
            {
                TaxCalculationResultId = Guid.NewGuid(),
                ApplicationUser = await ApplicationUserDAL.GetApplicationUserByUsername(_dbContext, emailAddress),
                PostalCode = _result.PostalCode!,
                TaxCalculationType = _result.TaxCalculationType!,
                IncomeRange = _result.IncomeRange!,
                TaxRate = _result.TaxRate!,
                PostalCodeValue = _result.PostalCode.PostalCodeValue!,
                TaxCalculationTypeValue = _result.TaxCalculationType.TaxCalculationTypeValue!,
                IncomeRangeFrom = _result.IncomeRange.IncomeRangeFrom!,
                IncomeRangeTo = _result.IncomeRange.IncomeRangeTo!,
                TaxRateValue = _result.TaxRate.TaxRateValue!,
                IsPercentage = _result.TaxRate.IsPercentage!,
                AnnualIncome = annualIncomeTaxReq.AnnualIncome!,
                CreationDate = DateTime.Now.Date
            };

            await _dbContext.AddAsync(_taxCalculationResult);

            await _dbContext.SaveChangesAsync();

            return FillAnnualIncomeTaxResp(_taxCalculationResult);
        }

        private AnnualIncomeTaxResp FillAnnualIncomeTaxResp(TaxCalculationResult taxCalculationResult)
        {
            return new AnnualIncomeTaxResp()
            {
                AnnualIncome = taxCalculationResult.AnnualIncome,
                AnnualIncomeTax = taxCalculationResult.AnnualIncomeTax,
                PostalCodeValue = taxCalculationResult.PostalCodeValue,
                CreationDate = taxCalculationResult.CreationDate,
            };
        }

    }
}
