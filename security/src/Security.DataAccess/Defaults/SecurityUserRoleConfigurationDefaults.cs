namespace Security.DataAccess
{
    internal static class SecurityUserRoleConfigurationDefaults
    {
        public static string TableName { get; } = "SecurityUserRole";

        public static string IdentifierColumnName { get; } = "ID";

        public static string UserIdentifierColumnName { get; } = "UserID";

        public static string RoleIdentifierColumnName { get; } = "RoleID";
    }
}