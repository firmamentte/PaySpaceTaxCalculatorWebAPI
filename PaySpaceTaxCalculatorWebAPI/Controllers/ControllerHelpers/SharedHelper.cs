using Microsoft.Extensions.Primitives;

namespace PaySpaceTaxCalculatorWebAPI.Controllers.ControllerHelpers
{
    public class SharedHelper
    {
        public string? GetHeaderEmailAddress(HttpRequest request)
        {
            return request.Headers.TryGetValue("EmailAddress", out StringValues _username) ? _username.FirstOrDefault() : null;
        }

        public bool IsEmailAddress(string emailAddress)
        {
            return FirmamentUtilities.Utilities.EmailHelper.IsEmailAddress(emailAddress);
        }
    }
}
