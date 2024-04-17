using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Authentication.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class SimpleAuthenticationController : ControllerBase
    {
        private readonly IDataProtector dataProtector;

        public SimpleAuthenticationController(IDataProtectionProvider dataProtectorProvider)
        {
            this.dataProtector = dataProtectorProvider.CreateProtector(AuthenticationDefaults.CustomCookieAuthentication);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SimpleSignIn()
        {
            var encoded = this.dataProtector.Protect(AuthenticationDefaults.AuthenticationType);
            this.HttpContext.Response.Cookies.Append(AuthenticationDefaults.CustomCookieAuthentication, encoded);

            return this.Ok();
        }

        [HttpGet]
        [Authorize]
        public ActionResult SimpleSignOut()
        {
            this.HttpContext.Response.Cookies.Delete(AuthenticationDefaults.CustomCookieAuthentication);

            return this.Ok();
        }
    }
}