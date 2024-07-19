using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Security.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Security
{
    public class SecurityUserEmailValidator<TUser, TKey> : IUserValidator<TUser>
        where TKey : IEquatable<TKey>
        where TUser : SecurityUser<TKey>
    {
        protected SecurityOptions SecurityOptions { get; private init; }
        
        protected IdentityErrorDescriber ErrorDescriber { get; private init; }
        
        public SecurityUserEmailValidator(IOptionsMonitor<SecurityOptions> optionsMonitor, IdentityErrorDescriber errorDescriber)
        {
            ArgumentNullException.ThrowIfNull(optionsMonitor, nameof(optionsMonitor));
            this.SecurityOptions = optionsMonitor.CurrentValue ?? throw new ArgumentNullException(nameof(optionsMonitor.CurrentValue));

            this.ErrorDescriber = errorDescriber ?? new IdentityErrorDescriber();
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            string normalizedEmail = user.NormalizedEmail;

            if (SecurityOptions.UserOptions.EmailValidationOptions.AllowedDomains.Any(domain => normalizedEmail.EndsWith($"@{domain}")))
            {
                return await Task.FromResult(IdentityResult.Success);
            }

            return IdentityResult.Failed(this.ErrorDescriber.NotAllowedEmailDomain());
        }
    }
}