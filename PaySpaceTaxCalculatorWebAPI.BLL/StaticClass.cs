using Microsoft.Extensions.Configuration;
using static PaySpaceTaxCalculatorWebAPI.Data.StaticClass;

namespace PaySpaceTaxCalculatorWebAPI.BLL
{
    public static class StaticClass
    {
        public static void InitializeApplicationSettings(IConfiguration configuration)
        {
            DatabaseHelper.ConnectionString ??= configuration["ConnectionStrings:DatabasePath"];
        }
    }
}
