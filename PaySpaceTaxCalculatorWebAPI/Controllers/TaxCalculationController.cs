
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using PaySpaceTaxCalculatorWebAPI.BLL.BLL;
using PaySpaceTaxCalculatorWebAPI.BLL.DataContract;
using PaySpaceTaxCalculatorWebAPI.Controllers.ControllerHelpers;
using PaySpaceTaxCalculatorWebAPI.Filters;
using System.Net;

namespace PaySpaceTaxCalculatorWebAPI.Controllers
{
    [Route("api/TaxCalculation")]
    [ApiController]
    [AuthorizeApplicationUser]
    public class TaxCalculationController : ControllerBase
    {
        private readonly TaxCalculationBLL TaxCalculationBLL;
        private readonly SharedHelper SharedHelper;

        public TaxCalculationController()
        {
            TaxCalculationBLL = new();
            SharedHelper = new SharedHelper();
        }

        [Route("V1/GetTaxCalculationsByEmailAddress")]
        [HttpGet]
        public async Task<ActionResult> GetTaxCalculationsByEmailAddress()
        {
            #region RequestValidation

            ModelState.Clear();

            #endregion

            return Ok(await TaxCalculationBLL.GetTaxCalculationsByEmailAddress(SharedHelper.GetHeaderEmailAddress(Request)!));
        }

        [Route("V1/CreateAnnualIncomeTax")]
        [HttpPost]
        public async Task<ActionResult> CreateAnnualIncomeTax(AnnualIncomeTaxReq annualIncomeTaxReq)
        {
            #region RequestValidation

            ModelState.Clear();

            if (annualIncomeTaxReq is null)
            {
                ModelState.AddModelError("AnnualIncomeTaxReq", "Annual Income Tax request can not be null");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(annualIncomeTaxReq.PostalCode))
                {
                    ModelState.AddModelError("PostalCode", "Postal Code required");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            return Created(string.Empty, await TaxCalculationBLL.CreateAnnualIncomeTax(SharedHelper.GetHeaderEmailAddress(Request)!, annualIncomeTaxReq!));
        }
    }
}
