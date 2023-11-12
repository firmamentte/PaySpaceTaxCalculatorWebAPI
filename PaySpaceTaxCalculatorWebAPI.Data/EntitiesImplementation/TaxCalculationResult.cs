namespace PaySpaceTaxCalculatorWebAPI.Data.Entities;

public partial class TaxCalculationResult
{
    public virtual decimal AnnualIncomeTax
    {
        get
        {
            if (TaxRate.IsPercentage)
                return Math.Round(AnnualIncome * (TaxRateValue / 100), 2);
            else
                return TaxRateValue;
        }
    }
}
