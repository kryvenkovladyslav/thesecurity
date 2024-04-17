using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.WebApi
{
    public class SimpleCookieAuthenticationMiddleware : BaseMiddleware
    {
        private readonly IDataProtector dataProtector;
        public SimpleCookieAuthenticationMiddleware(RequestDelegate next,IDataProtectionProvider dataProtectionProvider) : base(next)
        {
            this.dataProtector = dataProtectionProvider.CreateProtector(AuthenticationDefaults.CustomCookieAuthentication);
        }

        public async override Task Invoke(HttpContext context)
        {
            var encodedCookie = context.Request.Cookies[AuthenticationDefaults.CustomCookieAuthentication];

            if (encodedCookie == null)
            {
                await this.Next(context);
                return;
            }

            var decodedCookie = this.dataProtector.Unprotect(encodedCookie);

            var defaultAnonymousClaim = new Claim(ClaimTypes.Anonymous, AuthenticationDefaults.DefaultAuthenticationClaim);
            var defaultClaimsIdentity = new ClaimsIdentity(new[] { defaultAnonymousClaim }, decodedCookie);
            var principal = new ClaimsPrincipal(defaultClaimsIdentity);

            context.User = principal;

            await this.Next.Invoke(context);
        }
    }
}