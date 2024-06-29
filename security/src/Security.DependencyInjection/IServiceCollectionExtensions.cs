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
            services
                .AddConfirmationService<IEmailConfirmationService, IEmailConfirmationMessage, SecurityEmailConfirmationService>()
                .AddConfirmationService<IPhoneNumberConfirmationService, IPhoneNumberConfirmationMessage, SecurityPhoneNumberConfirmationService>();

            services.AddIdentityCore<TUser>(options =>
            {
                options.Tokens.ChangePhoneNumberTokenProvider = SecurityTokenOptions.DefaultSecurityPhoneNumberTokenProvider;
                options.Tokens.EmailConfirmationTokenProvider = SecurityTokenOptions.DefaultSecurityEmailTokenProvider;
                options.Tokens.ChangeEmailTokenProvider = SecurityTokenOptions.DefaultSecurityEmailTokenProvider;
            })
                .AddTokenProvider<SecurityEmailConfirmationTokenProvider<TUser, TIdentifier>>(SecurityTokenOptions.DefaultSecurityEmailTokenProvider)
                .AddTokenProvider<SecurityPhoneNumberConfirmationTokenProvider<TUser, TIdentifier>>(SecurityTokenOptions.DefaultSecurityPhoneNumberTokenProvider);

            return services;
        }

        public static IServiceCollection AddConfirmationService<TInterface, TConfirmationMessage, TImplementation>(this IServiceCollection services)
            where TImplementation : class, TInterface
            where TConfirmationMessage : IConfirmationMessage
            where TInterface : class, IContactConfirmationService<TConfirmationMessage>
        {
            services.TryAddScoped<TInterface, TImplementation>();
            return services;
        }

        public static IServiceCollection AddEntityFrameworkSecurityStores
                <TContext, TUser, TRole, TClaim, TUserRole, TRoleClaim, TIdentifier>(this IServiceCollection services)
            where TUser : SecurityUser<TIdentifier>, new()
            where TRole : SecurityRole<TIdentifier>, new()
            where TClaim : SecurityClaim<TIdentifier>, new()
            where TUserRole : SecurityUserRole<TIdentifier>, new()
            where TRoleClaim : SecurityRoleClaim<TIdentifier>, new()
            where TIdentifier : IEquatable<TIdentifier>
            where TContext : SecurityDatabaseContext<TUser, TRole, TClaim, TUserRole, TRoleClaim, TIdentifier>
        {
            services.TryAddScoped<IUserStore<TUser>, SecurityUserStore<TContext, TUser, TClaim, TIdentifier>>();

            return services;
        }
    }
}