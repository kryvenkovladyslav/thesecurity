using Microsoft.AspNetCore.Identity;
using Security.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Security
{
    public class SecurityUserPasswordValidator<TUser, TIdentifier> : PasswordValidator<TUser>
        where TUser : SecurityUser<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var baseValidationResult = await base.ValidateAsync(manager, user, password);

            if (!baseValidationResult.Succeeded)
            {
                return baseValidationResult;
            }

            var errorList = baseValidationResult.Errors.ToList();

            if(password == "super_secret")
            {
                errorList.Add(new IdentityError
                {
                    Code = "InvalidPassword",
                    Description = "This password is not allowed"
                });
            }

            return errorList.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errorList.ToArray());
        }
    }
}