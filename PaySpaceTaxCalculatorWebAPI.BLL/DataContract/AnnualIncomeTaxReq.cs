namespace PaySpaceTaxCalculatorWebAPI.BLL.DataContract
{
    public class AnnualIncomeTaxReq
    {
        public string PostalCode { get; set; } = string.Empty;
        public decimal AnnualIncome { get; set; } = decimal.Zero;
    }
}
