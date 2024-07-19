using Microsoft.AspNetCore.Identity;

namespace Security
{
    public static class IdentityErrorDescriberExtensions
    {
        public static IdentityError NotAllowedEmailDomain(this IdentityErrorDescriber errorDescriber)
        {
            return new IdentityError
            {
                Code = nameof(NotAllowedEmailDomain),
                Description = "This domain is not allowed"
            };
        }
    }
}