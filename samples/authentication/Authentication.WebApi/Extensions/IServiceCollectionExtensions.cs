using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.WebApi
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <see cref="SimpleCookieSignInOutService"/> to the DI Container
        /// </summary>
        /// <param name="app">Represents <see cref="IServiceCollection"/></param>
        /// <returns>Configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSimpleCookieSignInOutService(this IServiceCollection services)
        {
            services.AddScoped<ISimpleSignInOutService, SimpleCookieSignInOutService>();

            return services;
        }

        /// <summary>
        /// Adds <see cref="SimpleSignInOutService"/> to the DI Container
        /// </summary>
        /// <param name="app">Represents <see cref="IServiceCollection"/></param>
        /// <returns>Configured <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSimpleSignInOutService(this IServiceCollection services)
        {
            services.AddScoped<ISimpleSignInOutService, SimpleSignInOutService>();

            return services;
        }
    }
}