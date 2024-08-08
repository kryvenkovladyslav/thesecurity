using Microsoft.AspNetCore.Identity;
using Security.Abstract;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Security
{
    public class SecurityUserRoleClaimsPrincipalFactory<TSecurityUser, TSecurityRole, TIdentifier> :
        SecurityUserClaimsPrincipalFactory<TSecurityUser, TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityUser : SecurityUser<TIdentifier>
        where TSecurityRole : SecurityRole<TIdentifier>
    {
        protected virtual RoleManager<TSecurityRole> RoleManager { get; private init; }

        public SecurityUserRoleClaimsPrincipalFactory(UserManager<TSecurityUser> userManager, RoleManager<TSecurityRole> roleManager) 
            : base(userManager)
        {
            this.RoleManager = roleManager ?? throw new ArgumentException(nameof(roleManager));
        }

        protected override async Task<ClaimsIdentity> GenerateUserClaimsIdentityAsync(TSecurityUser user)
        {
            var claimsIdentity = await base.GenerateUserClaimsIdentityAsync(user);

            if (!this.RoleManager.SupportsRoleClaims) 
            {
                return claimsIdentity;
            }

            var userRoles = await this.UserManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                var requiredRole = await this.RoleManager.FindByNameAsync(userRole);
                var roleClaims = await this.RoleManager.GetClaimsAsync(requiredRole);
                claimsIdentity.AddClaims(roleClaims);
            }

            return claimsIdentity;
        }
    }
}