using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using PaySpaceTaxCalculatorWebAPI.BLL.BLL;
using PaySpaceTaxCalculatorWebAPI.BLL.DataContract;
using PaySpaceTaxCalculatorWebAPI.Controllers.ControllerHelpers;
using System.Net;

namespace PaySpaceTaxCalculatorWebAPI.Controllers
{
    [Route("api/ApplicationUser")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly ApplicationUserBLL ApplicationUserBLL;
        private readonly SharedHelper SharedHelper;

        public ApplicationUserController()
        {
            ApplicationUserBLL = new();
            SharedHelper = new SharedHelper();
        }

        [Route("V1/SignUp")]
        [HttpPost]
        public async Task<ActionResult> SignUp()
        {
            #region RequestValidation

            ModelState.Clear();

            if (!Request.Headers.TryGetValue("EmailAddress", out StringValues _emailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Email Address required");
            }
            else
            {
                if (!SharedHelper.IsEmailAddress(_emailAddress.FirstOrDefault()!))
                {
                    ModelState.AddModelError("InvalidEmailAddress", "Invalid Email Address");
                }
            }

            if (!Request.Headers.TryGetValue("UserPassword", out StringValues _userPassword))
            {
                ModelState.AddModelError("UserPassword", "User Password required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            await ApplicationUserBLL.SignUp(_emailAddress.FirstOrDefault()!, _userPassword.FirstOrDefault()!);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [Route("V1/Authenticate")]
        [HttpPost]
        public async Task<ActionResult> Authenticate()
        {
            #region RequestValidation

            ModelState.Clear();

            if (!Request.Headers.TryGetValue("EmailAddress", out StringValues _emailAddress))
            {
                ModelState.AddModelError("EmailAddress", "Email Address required");
            }

            if (!Request.Headers.TryGetValue("UserPassword", out StringValues _userPassword))
            {
                ModelState.AddModelError("UserPassword", "User Password required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResp(ModelState));
            }

            #endregion

            AuthenticateResp _authenticateResp = await ApplicationUserBLL.Authenticate(_emailAddress.FirstOrDefault()!, _userPassword.FirstOrDefault()!);

            Response.Headers.Add("AccessToken", _authenticateResp.AccessToken);
            Response.Headers.Add("AccessTokenExpiryDate", _authenticateResp.AccessTokenExpiryDate.ToString());

            return Ok();
        }
    }
}
