using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Security.Abstract;

namespace Security
{
    public abstract class SecurityTokenProvider<TUser, TIdentifier> : IUserTwoFactorTokenProvider<TUser>
        where TUser : SecurityUser<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public virtual int CodeLength { get; private set; } = 12;

        public virtual Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        {
            return Task.FromResult(manager.SupportsUserSecurityStamp);
        }

        public virtual Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
        {
            var securityCode = this.GenerateSecurityCode(purpose, user);
            return Task.FromResult(securityCode);
        }

        public virtual Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
        {
            var securityCode = this.GenerateSecurityCode(purpose, user).Equals(token);
            return Task.FromResult(securityCode);
        }

        protected virtual string GenerateSecurityCode(string purpose, TUser user)
        {
            var securityData = this.GetData(purpose, user);
            var hashAlgorithm = new HMACSHA1(Encoding.UTF8.GetBytes(user.SecurityStamp));

            var hashCode = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(securityData));
            return BitConverter.ToString(hashCode[^CodeLength..]).Replace("-", "");
        }

        protected virtual string GetData(string purpose, TUser user)
        {
            return $"{purpose}{user.SecurityStamp}";
        } 
    }
}