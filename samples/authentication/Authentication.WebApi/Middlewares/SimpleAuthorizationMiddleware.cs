using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.WebApi
{
    /// <summary>
    /// This class is only for test purposes. 
    /// It performs anonymous authorization per the request via Authorize Attribute
    /// </summary>
    public sealed class SimpleAuthorizationMiddleware : BaseMiddleware
    {
        /// <summary>
        /// The constructor for creating the instance
        /// </summary>
        /// <param name="next">The request delegate</param>
        public SimpleAuthorizationMiddleware(RequestDelegate next) : base(next) { }

        /// <summary>
        /// Invokes this middleware component
        /// </summary>
        /// <param name="context">The context describes current HTTP request</param>
        public async override Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            var allowAnonymousMetaData = endpoint?.Metadata.GetOrderedMetadata<IAllowAnonymous>();
            if (allowAnonymousMetaData != null && allowAnonymousMetaData.Count != 0)  
            {
                await this.Next.Invoke(context);
                return;
            }

             var authorizeMetaData = endpoint?.Metadata.GetOrderedMetadata<IAuthorizeData>();

            if (authorizeMetaData != null && authorizeMetaData.Count != 0)
            {
                var identity = context.User.Identity;
                if (!identity.IsAuthenticated)
                {
                    this.Challenge(context);
                    return;
                }

                foreach (var currentIdentity in context.User.Identities)
                {
                    var requiredAuthorizationClaim = currentIdentity.Claims
                        .Where(claim => claim.Value == AuthenticationDefaults.AuthorizingClaim)
                        .ToArray();

                    if(requiredAuthorizationClaim != null && requiredAuthorizationClaim.Count() != 0)
                    {
                        await this.Next(context);
                        return;
                    }
                }

                this.Forbid(context);
                return;
            }

            await this.Next.Invoke(context);
        }

        /// <summary>
        /// Sets Unauthorized status code to the response
        /// </summary>
        /// <param name="context">The context describes a current request</param>
        private void Challenge(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }

        /// <summary>
        /// Sets Forbidden status code to the response
        /// </summary>
        /// <param name="context">The context describes a current request</param>
        private void Forbid(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}