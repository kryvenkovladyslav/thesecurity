using Security.Abstract;

namespace Security.DataAccess
{
    /// <summary>
    /// Provides constants for customizing <see cref="SecurityUserRole{TIdentifier}"/> table
    /// </summary>
    internal static class SecurityUserRoleConfigurationDefaults
    {
        /// <summary>
        /// Provides a name of the table
        /// </summary>
        public static string TableName { get; } = "SecurityUserRole";

        /// <summary>
        /// Provides the name of the column representing the primary key
        /// </summary>
        public static string IdentifierColumnName { get; } = "ID";

        /// <summary>
        /// Provides the name of the column representing the foreign key
        /// </summary>
        public static string UserIdentifierColumnName { get; } = "UserID";

        /// <summary>
        /// Provides the name of the column representing the foreign key
        /// </summary>
        public static string RoleIdentifierColumnName { get; } = "RoleID";
    }
}