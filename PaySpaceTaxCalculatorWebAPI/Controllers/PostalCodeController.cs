using Microsoft.AspNetCore.Mvc;
using PaySpaceTaxCalculatorWebAPI.BLL.BLL;
using PaySpaceTaxCalculatorWebAPI.Filters;

namespace PaySpaceTaxCalculatorWebAPI.Controllers
{
    [Route("api/PostalCode")]
    [ApiController]
    [AuthorizeApplicationUser]
    public class PostalCodeController : ControllerBase
    {
        private readonly PostalCodeBLL PostalCodeBLL;
        public PostalCodeController()
        {
            PostalCodeBLL = new();
        }

        [Route("V1/GetPostalCodes")]
        [HttpGet]
        public async Task<ActionResult> GetPostalCodes()
        {
            #region RequestValidation

            ModelState.Clear();

            #endregion

            return Ok(await PostalCodeBLL.GetPostalCodes());
        }
    }
}
