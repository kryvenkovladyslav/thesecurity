namespace Authentication.WebApi
{
    /// <summary>
    /// Provides constant values 
    /// </summary>
    public static class AuthenticationDefaults
    {
        /// <summary>
        /// Represents a default authentication type for <see cref="SimpleAuthenticationMiddleware"/>
        /// </summary>
        public static string AuthenticationType { get; } = "anonymous-authentication";

        /// <summary>
        /// Represents a default authentication claim for <see cref="SimpleAuthenticationMiddleware"/>
        /// </summary>
        public static string DefaultAuthenticationClaim { get; } = "anonymous-authentication-claim";

        public static string AuthorizingClaim { get; } = "anonymous-authorizing-claim";

        public static string AuthorizingType { get; } = "anonymous-authorizing";

        public static string CustomCookieAuthentication { get; } = "cookie-authentication";
    }
}