using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Security.EntityFrameworkStores;
using Security.Abstract;
using Security.DataAccess;
using System;
using Microsoft.AspNetCore.Authentication;

namespace Security.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSecuritySystem<TUser, TRole, TIdentifier>(this IServiceCollection services)
            where TUser : SecurityUser<TIdentifier>
            where TRole : SecurityRole<TIdentifier>
            where TIdentifier : IEquatable<TIdentifier> 
        {
            services.AddIdentityCore<TUser>(options =>
            {
                options.Tokens.ChangePhoneNumberTokenProvider = SecurityTokenOptions.DefaultSecurityPhoneNumberTokenProvider;
                options.Tokens.EmailConfirmationTokenProvider = SecurityTokenOptions.DefaultSecurityEmailTokenProvider;
                options.Tokens.ChangeEmailTokenProvider = SecurityTokenOptions.DefaultSecurityEmailTokenProvider;
                options.Tokens.PasswordResetTokenProvider = SecurityTokenOptions.DefaultSecurityEmailTokenProvider;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 10;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
            })
                .AddRoles<TRole>()
                .AddTokenProvider<SecurityEmailConfirmationTokenProvider<TUser, TIdentifier>>(SecurityTokenOptions.DefaultSecurityEmailTokenProvider)
                .AddTokenProvider<SecurityPhoneNumberConfirmationTokenProvider<TUser, TIdentifier>>(SecurityTokenOptions.DefaultSecurityPhoneNumberTokenProvider)
                .AddSignInManager<SignInManager<TUser>>();

            services.AddScoped<IUserClaimsPrincipalFactory<TUser>, SecurityUserRoleClaimsPrincipalFactory<TUser, TRole, TIdentifier>>();
            services.AddScoped<IPasswordHasher<TUser>, SecurityPasswordHasher<TUser, TIdentifier>>();

            services
                .AddConfirmationService<IEmailConfirmationService, IEmailConfirmationMessage, SecurityEmailConfirmationService>()
                .AddConfirmationService<IPhoneNumberConfirmationService, IPhoneNumberConfirmationMessage, SecurityPhoneNumberConfirmationService>();
            services.AddScoped<IPasswordValidator<TUser>, SecurityUserPasswordValidator<TUser, TIdentifier>>();

            return services;
        }

        public static AuthenticationBuilder AddSecurityAuthentication(this IServiceCollection services, string authenticationDefaultScheme = null)
        {
            return services.AddAuthentication(authenticationDefaultScheme ?? SecurityConstants.AuthenticationType);
        }

        public static AuthenticationBuilder AddSecurityCookieAuthentication(this IServiceCollection services)
        {
            return services.AddSecurityAuthentication(SecurityConstants.AuthenticationType)
                .AddCookie(SecurityConstants.AuthenticationType);
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
            services.TryAddScoped<IRoleStore<TRole>, SecurityRoleStore<TContext, TRole, TRoleClaim, TIdentifier>>();

            return services;
        }
    }
}