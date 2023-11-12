using PaySpaceTaxCalculatorWebAPI.BLL;

namespace PaySpaceTaxCalculatorWebAPI
{
    public static class PaySpaceTaxCalculatorWebAPIApp
    {
        public static void InitializeApplicationSettings(this WebApplication app)
        {
            StaticClass.InitializeApplicationSettings(app.Configuration);
        }
    }
}
