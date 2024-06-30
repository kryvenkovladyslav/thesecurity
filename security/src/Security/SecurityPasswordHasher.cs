using Microsoft.AspNetCore.Identity;
using Security.Abstract;
using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Security
{
    public class SecurityPasswordHasher<TUser, TIdentifier> : IPasswordHasher<TUser>
        where TUser : SecurityUser<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public virtual string HashPassword(TUser user, string password)
        {
            var convertedIdentifier = TypeDescriptor.GetConverter(typeof(string)).ConvertToInvariantString(user.ID);

            var hashAlgorithm = new HMACSHA256(Encoding.UTF8.GetBytes(convertedIdentifier));
            return BitConverter.ToString(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public virtual PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            return this.HashPassword(user, providedPassword) == hashedPassword ? 
                PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}