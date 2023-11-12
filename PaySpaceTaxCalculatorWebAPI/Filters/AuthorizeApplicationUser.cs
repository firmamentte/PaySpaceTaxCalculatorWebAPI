using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using PaySpaceTaxCalculatorWebAPI.BLL.BLL;
using System.Net;

namespace PaySpaceTaxCalculatorWebAPI.Filters
{
    public class AuthorizeApplicationUser : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ApplicationUserBLL ApplicationUserBLL;

        public AuthorizeApplicationUser()
        {
            ApplicationUserBLL = new();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            List<string> _errors = new();

            if (!context.HttpContext.Request.Headers.TryGetValue("AccessToken", out StringValues _accessToken))
                _errors.Add("Access Token required");

            if (!context.HttpContext.Request.Headers.TryGetValue("EmailAddress", out StringValues _emailAddress))
                _errors.Add("Email Address required");

            if (!_errors.Any())
            {
                if (!await ApplicationUserBLL.IsEmailAddressAccessTokenValid(_emailAddress.FirstOrDefault()!, _accessToken.FirstOrDefault()!))
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new JsonResult(new ApiErrorResp("Request Forbidden"));
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(new ApiErrorResp(_errors));
            }
        }
    }
}
