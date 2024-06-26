namespace Security.DataAccess
{
    internal static class SecurityUserConfigurationDefaults
    {
        public static string SecurityUserTableName { get; } = "SecurityUser";

        public static string IdentifierColumnName { get; } = "ID";

        public static string UserNameColumnName { get; } = "UserName";

        public static string PhoneNumberColumnName { get; } = "PhoneNumber";

        public static string PhoneNumberConfirmedColumnName { get; } = "PhoneNumberConfirmed";

        public static string EmailColumnName { get; } = "Email";

        public static string NormalizedEmailColumnName { get; } = "NormalizedEmail";

        public static string EmailConfirmedColumnName { get; } = "EmailConfirmed";

        public static string NormalizedUserNameColumnName { get; } = "NormalizedUserName";

        public static string SecurityStampColumnName { get; } = "SecurityStamp";

    }
}