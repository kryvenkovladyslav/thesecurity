using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.WebApi
{
    public sealed class SimpleSignInOutService : ISimpleSignInOutService
    {
        public async Task SignInAsync(HttpContext context)
        {
            var defaultAnonymousClaim = new Claim(ClaimTypes.Anonymous, AuthenticationDefaults.DefaultAuthenticationClaim);
            var defaultClaimsIdentity = new ClaimsIdentity(new[] { defaultAnonymousClaim }, AuthenticationDefaults.CustomCookieAuthentication);
            var principal = new ClaimsPrincipal(defaultClaimsIdentity);

            await context.SignInAsync(principal);
        }

        public async Task SignOutAsync(HttpContext context)
        {
            await context.SignOutAsync();
        }
    }
}