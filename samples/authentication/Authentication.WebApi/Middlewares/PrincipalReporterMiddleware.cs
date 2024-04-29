using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Authentication.WebApi
{
    /// <summary>
    /// This class is only for test purposes. 
    /// It provides logs into about authenticated request into the console 
    /// </summary>
    public sealed class PrincipalReporterMiddleware : BaseMiddleware
    {
        /// <summary>
        /// The logger for logging information into the console
        /// </summary>
        private readonly ILogger<PrincipalReporterMiddleware> logger;

        /// <summary>
        /// The constructor for creating the instance
        /// </summary>
        /// <param name="next">The request delegate</param>
        /// <param name="logger">The logger for logging information into the console</param>
        /// <exception cref="ArgumentNullException">ArgumentNullException is thrown if the logger is not provided</exception>
        public PrincipalReporterMiddleware(RequestDelegate next, ILogger<PrincipalReporterMiddleware> logger) : base(next) 
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Invokes this middleware component
        /// </summary>
        /// <param name="context">The context describes current HTTP request</param>
        public async override Task Invoke(HttpContext context)
        {
            var claimsPrincipal = context.User;

            this.logger.LogInformation($"Authenticated: {claimsPrincipal.Identity.IsAuthenticated}");
            this.logger.LogInformation($"Authentication Type: {claimsPrincipal.Identity.AuthenticationType}");
            this.logger.LogInformation($"Identities: {claimsPrincipal.Identities.Count()}");

            foreach (var identity in claimsPrincipal.Identities)
            {
                this.logger.LogInformation($"Authentication Type type: {identity.AuthenticationType}, {identity.Claims.Count()} claims");

                foreach (var claim in identity.Claims)
                {
                    this.logger.LogInformation($"Type: {this.GetName(claim.Type)}, Value: {claim.Value}, Issuer: {claim.Issuer}");
                }
            }

            await this.Next.Invoke(context);
        }

        /// <summary>
        /// Gets name for the claim type
        /// </summary>
        /// <param name="claimType">Required claim type</param>
        /// <returns>The extracted name</returns>
        private string GetName(string claimType)
        {
            return Path.GetFileName(new Uri(claimType).LocalPath);
        }
    }
}