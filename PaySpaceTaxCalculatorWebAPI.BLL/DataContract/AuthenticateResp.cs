namespace PaySpaceTaxCalculatorWebAPI.BLL.DataContract
{
    public class AuthenticateResp
    {
        public string? AccessToken { get; set; }
        public DateTime? AccessTokenExpiryDate { get; set; }
    }
}
