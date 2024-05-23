using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Security.EntityFrameworkStores;
using Security.Abstract;
using Security.DataAccess;
using System;

namespace Security.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSecuritySystem<TUser, TIdentifier>(this IServiceCollection services)
            where TUser : SecurityUser<TIdentifier>
            where TIdentifier : IEquatable<TIdentifier> 
        {

            services.TryAddScoped<IUserValidator<TUser>, SecurityUserEmailValidator<TUser, TIdentifier>>();
            services.AddIdentityCore<TUser>();

            return services;
        }

        public static IServiceCollection AddEntityFrameworkSecurityStores<TContext, TUser, TClaim, TIdentifier>(this IServiceCollection services)
            where TUser : SecurityUser<TIdentifier>
            where TClaim : SecurityClaim<TIdentifier>, new()
            where TIdentifier : IEquatable<TIdentifier>
            where TContext : SecurityDatabaseContext<TUser, TIdentifier, TClaim>
        {
            services.TryAddScoped<IUserStore<TUser>, SecurityUserStore<TContext, TUser, TClaim, TIdentifier>>();

            return services;
        }
    }
}