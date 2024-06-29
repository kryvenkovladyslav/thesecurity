using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Security.Abstract;

namespace Security
{
    public class SecurityPhoneNumberConfirmationTokenProvider<TUser, TIdentifier> : SecurityTokenProvider<TUser, TIdentifier>
        where TUser : SecurityUser<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public async override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        {
            var result = await base.CanGenerateTwoFactorTokenAsync(manager, user) &&
                !string.IsNullOrEmpty(user.PhoneNumber) &&
                !user.IsPhoneNumberConfirmed;

            return await Task.FromResult(result);
        }
    }
}