using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.WebApi
{
    /// <summary>
    /// This class is only for test purposes. 
    /// It performs anonymous authentication per the request via Authorize Attribute
    /// </summary>
    public sealed class SimpleAuthenticationMiddleware : BaseMiddleware
    {
        /// <summary>
        /// The constructor for creating the instance
        /// </summary>
        /// <param name="next">The request delegate</param>
        public SimpleAuthenticationMiddleware(RequestDelegate next) : base(next) { }

        /// <summary>
        /// Invokes this middleware component
        /// </summary>
        /// <param name="context">The context describes current HTTP request</param>
        public override async Task Invoke(HttpContext context)
        {
            var defaultAnonymousClaim = new Claim(ClaimTypes.Anonymous, AuthenticationDefaults.DefaultAuthenticationClaim);
            var defaultClaimsIdentity = new ClaimsIdentity(new[] { defaultAnonymousClaim }, AuthenticationDefaults.AuthenticationType);
            var principal = new ClaimsPrincipal(defaultClaimsIdentity);

            context.User = principal;

            await this.Next.Invoke(context);
        }
    }
}