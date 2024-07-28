namespace Security.DataAccess
{
    internal static class SecurityUserClaimConfigurationDefaults
    {
        public static string TableName { get; } = "SecurityUserClaim";

        public static string IdentifierColumnName { get; } = "ID";

        public static string UserIdentifierColumnName { get; } = "UserID";

        public static string TypeColumnName { get; } = "Type";

        public static string ValueColumnName { get; } = "Value";
    }
}