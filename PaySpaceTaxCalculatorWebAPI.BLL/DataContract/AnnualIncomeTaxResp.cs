
namespace PaySpaceTaxCalculatorWebAPI.BLL.DataContract
{
    public class AnnualIncomeTaxResp
    {
        public decimal AnnualIncome { get; set; } = decimal.Zero;
        public decimal AnnualIncomeTax { get; set; } = decimal.Zero;
        public string PostalCodeValue { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
    }
}
