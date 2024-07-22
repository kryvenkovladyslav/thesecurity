namespace Security.DataAccess
{
    internal static class SecurityRoleConfigurationDefaults
    {
        public static string TableName { get; } = "SecurityRole";

        public static string IdentifierColumnName { get; } = "ID";

        public static string NameColumnName { get; } = "Name";

        public static string NormalizedNameColumnName { get; } = "NormalizedName";

        public static string ConcurrencyStampColumnName { get; } = "ConcurrencyStamp";
    }
}