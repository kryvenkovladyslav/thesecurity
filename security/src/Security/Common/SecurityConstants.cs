using Microsoft.AspNetCore.Identity;

namespace Security
{
    public static class SecurityConstants
    {
        public static string AuthenticationType { get; } = IdentityConstants.ApplicationScheme;
    }
}