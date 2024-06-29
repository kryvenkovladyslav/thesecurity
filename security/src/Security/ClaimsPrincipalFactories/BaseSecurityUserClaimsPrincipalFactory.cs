using Microsoft.AspNetCore.Identity;
using Security.Abstract;
using System;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Security
{
    public abstract class BaseSecurityUserClaimsPrincipalFactory<TUser, TIdentifier> : IUserClaimsPrincipalFactory<TUser>
        where TUser : SecurityUser<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        protected UserManager<TUser> UserManager { get; private init; }

        public BaseSecurityUserClaimsPrincipalFactory(UserManager<TUser> userManager)
        {
            this.UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        protected virtual async Task<ClaimsIdentity> GenerateUserClaimsIdentityAsync(TUser user)
        {
            var claimsIdentity = new ClaimsIdentity(SecurityConstants.AuthenticationType);

            var convertedIdentifier = TypeDescriptor.GetConverter(typeof(string)).ConvertToInvariantString(user.ID);

            claimsIdentity.AddClaims([
                new Claim(ClaimTypes.NameIdentifier, convertedIdentifier),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            ]);

            if (this.UserManager.SupportsUserClaim)
            {
                var userClaims = await this.UserManager.GetClaimsAsync(user).ConfigureAwait(false);
                claimsIdentity.AddClaims(userClaims);
            }

            return claimsIdentity;
        }

        public async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var claimsIdentity = await GenerateUserClaimsIdentityAsync(user);
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}