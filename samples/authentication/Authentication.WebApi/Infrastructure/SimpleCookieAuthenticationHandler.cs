using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.WebApi
{
    public sealed class SimpleCookieAuthenticationHandler : IAuthenticationHandler
    {
        private HttpContext context;

        private AuthenticationScheme scheme;

        private readonly IDataProtector dataProtector;

        public SimpleCookieAuthenticationHandler(IDataProtectionProvider dataProtectionProvider)
        {
            this.dataProtector = dataProtectionProvider.CreateProtector(AuthenticationDefaults.CustomCookieAuthentication);
        }

        public Task<AuthenticateResult> AuthenticateAsync()
        {
            var encodedCookie = context.Request.Cookies[AuthenticationDefaults.CustomCookieAuthentication];

            if (encodedCookie == null)
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var decodedCookie = this.dataProtector.Unprotect(encodedCookie);

            var defaultAnonymousClaim = new Claim(ClaimTypes.Anonymous, AuthenticationDefaults.DefaultAuthenticationClaim);
            var defaultClaimsIdentity = new ClaimsIdentity(new[] { defaultAnonymousClaim }, decodedCookie);
            var principal = new ClaimsPrincipal(defaultClaimsIdentity);

            var authenticationTicket = new AuthenticationTicket(principal, AuthenticationDefaults.CustomCookieAuthentication);

            return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }

        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            this.context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            this.context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        }

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            this.scheme = scheme;
            this.context = context;

            return Task.CompletedTask;
        }
    }
}
