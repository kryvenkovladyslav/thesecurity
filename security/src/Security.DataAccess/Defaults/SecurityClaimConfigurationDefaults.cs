namespace Security.DataAccess
{
    internal static class SecurityClaimConfigurationDefaults
    {
        public static string SecurityClaimTableName { get; } = "SecurityClaim";

        public static string IdentifierColumnName { get; } = "ID";

        public static string UserIdentifierColumnName { get; } = "UserID";

        public static string TypeColumnName { get; } = "Type";

        public static string ValueColumnName { get; } = "Value";
    }
}