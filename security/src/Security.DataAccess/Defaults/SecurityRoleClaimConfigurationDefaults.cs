namespace Security.DataAccess
{
    internal static class SecurityRoleClaimConfigurationDefaults
    {
        public static string TableName { get; } = "SecurityRoleClaim";

        public static string IdentifierColumnName { get; } = "ID";

        public static string RoleIdentifierColumnName { get; } = "RoleID";

        public static string ClaimTypeColumnName { get; } = "Type";

        public static string ClaimValueColumnName { get; } = "Value";
    }
}