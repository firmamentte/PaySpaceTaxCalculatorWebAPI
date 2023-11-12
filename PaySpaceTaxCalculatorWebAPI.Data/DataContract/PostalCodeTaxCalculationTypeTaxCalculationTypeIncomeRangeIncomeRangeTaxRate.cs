using PaySpaceTaxCalculatorWebAPI.Data.Entities;

namespace PaySpaceTaxCalculatorWebAPI.Data.DataContract
{
    public class PostalCodeTaxCalculationTypeIncomeRangeTaxRate
    {
        public PostalCode? PostalCode { get; set; }
        public TaxCalculationType? TaxCalculationType { get; set; }
        public IncomeRange? IncomeRange { get; set; }
        public TaxRate? TaxRate { get; set; }
    }
}
