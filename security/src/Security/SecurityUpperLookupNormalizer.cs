using Microsoft.AspNetCore.Identity;

namespace Security
{
    /// <summary>
    /// Provides functionality or normalizing keys (emails/names) for lookup purposes
    /// </summary>
    public sealed class SecurityUpperLookupNormalizer : ILookupNormalizer
    {
        /// <summary>
        /// Returns a normalized representation of the specified <paramref name="email"/>
        /// </summary>
        /// <param name="email">The email to normalize</param>
        /// <returns>A normalized representation of the specified <paramref name="email"/></returns>
        public string NormalizeEmail(string email)
        {
            return this.Normalize(email);
        }

        /// <summary>
        /// Returns a normalized representation of the specified <paramref name="name"/>
        /// </summary>
        /// <param name="name">The name to normalize</param>
        /// <returns>A normalized representation of the specified <paramref name="name"/></returns>
        public string NormalizeName(string name)
        {
            return this.Normalize(name);
        }

        /// <summary>
        /// Returns a normalized representation of the specified <paramref name="key"/>
        /// </summary>
        /// <param name="key">The string to normalize</param>
        /// <returns>A normalized representation of the specified <paramref name="key"/></returns>
        private string Normalize(string key)
        {
            return key.ToUpperInvariant();
        }
    }
}