using Microsoft.AspNetCore.Identity;
using Security.Abstract;
using System;

namespace Security
{
    public class SecurityUserClaimsPrincipalFactory<TUser, TIdentifier> : BaseSecurityUserClaimsPrincipalFactory<TUser, TIdentifier>
        where TUser : SecurityUser<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public SecurityUserClaimsPrincipalFactory(UserManager<TUser> userManager) : base(userManager) { }
    }
}