namespace Security
{
    public sealed class SecurityTokenOptions
    {
        public static string DefaultSecurityTokenProvider { get; } = "Default";

        public static string DefaultSecurityEmailTokenProvider { get; } = "SecurityEmail";

        public static string DefaultSecurityPhoneNumberTokenProvider { get; } = "SecurityPhone";

        public static string DefaultSecurityAuthenticatorProvider { get; } = "SecurityAuthenticator";

        public string SecurityEmailConfirmationTokenProvider { get; set; } = DefaultSecurityEmailTokenProvider;

        public string SecurityPasswordResetTokenProvider { get; set; } = DefaultSecurityTokenProvider;

        public string SecurityChangeEmailTokenProvider { get; set; } = DefaultSecurityEmailTokenProvider;

        public string SecurityChangePhoneNumberTokenProvider { get; set; } = DefaultSecurityPhoneNumberTokenProvider;

        public string SecurityAuthenticatorTokenProvider { get; set; } = DefaultSecurityAuthenticatorProvider;
    }
}