using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Authentication.WebApi
{
    public sealed class SimpleCookieSignInOutService : ISimpleSignInOutService
    {
        private readonly IDataProtector dataProtector;

        public SimpleCookieSignInOutService(IDataProtectionProvider dataProtectorProvider) 
        {
            this.dataProtector = dataProtectorProvider.CreateProtector(AuthenticationDefaults.CustomCookieAuthentication);
        }

        public Task SignInAsync(HttpContext context)
        {
            var encoded = this.dataProtector.Protect(AuthenticationDefaults.CustomCookieAuthentication);
            context.Response.Cookies.Append(AuthenticationDefaults.CustomCookieAuthentication, encoded);

            return Task.CompletedTask;
        }

        public Task SignOutAsync(HttpContext context)
        {
            context.Response.Cookies.Delete(AuthenticationDefaults.CustomCookieAuthentication);
            return Task.CompletedTask;
        }
    }
}