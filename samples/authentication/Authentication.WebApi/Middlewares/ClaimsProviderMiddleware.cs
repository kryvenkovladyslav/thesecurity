using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.WebApi
{
    /// <summary>
    /// This class is only for test purposes. 
    /// It sets up the required authorization claim
    /// </summary>
    public sealed class ClaimsProviderMiddleware : BaseMiddleware
    {
        /// <summary>
        /// The constructor for creating the instance
        /// </summary>
        /// <param name="next">The request delegate</param>
        public ClaimsProviderMiddleware(RequestDelegate next) : base(next) { }

        /// <summary>
        /// Invokes this middleware component
        /// </summary>
        /// <param name="context">The context describes current HTTP request</param>
        public override async Task Invoke(HttpContext context)
        {
            var identity = context.User.Identity;
            
            if (identity != null && identity.IsAuthenticated)
            {
                var claim = new Claim(ClaimTypes.AuthorizationDecision, AuthenticationDefaults.AuthorizingClaim);
                var claimsIdentity = new ClaimsIdentity(new[] { claim }, AuthenticationDefaults.AuthenticationType);
                context.User.AddIdentity(claimsIdentity);
            }

            await this.Next(context);
        }
    }
}