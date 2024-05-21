using System.Collections.Generic;

namespace Security
{
    public sealed class SecurityEmailValidationOptions
    {
        public IEnumerable<string> AllowedDomains { get; set; }
    }
}