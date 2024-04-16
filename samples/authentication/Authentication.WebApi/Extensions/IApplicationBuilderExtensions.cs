using Microsoft.AspNetCore.Builder;

namespace Authentication.WebApi
{
    /// <summary>
    /// Provides extension methods for <see cref="IApplicationBuilder"/>
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds <see cref="PrincipalReporterMiddleware"/> to the request pipeline
        /// </summary>
        /// <param name="app">Represents <see cref="IApplicationBuilder"/></param>
        /// <returns>Configured <see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UsePrincipalReporter(this IApplicationBuilder app)
        {
            app.UseMiddleware<PrincipalReporterMiddleware>();

            return app;
        }

        /// <summary>
        /// Adds <see cref="SimpleAuthenticationMiddleware"/> to the request pipeline
        /// </summary>
        /// <param name="app">Represents <see cref="IApplicationBuilder"/></param>
        /// <returns>Configured <see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseSimpleAuthentication(this IApplicationBuilder app)
        {
            app.UseMiddleware<SimpleAuthenticationMiddleware>();

            return app;
        }
    }
}