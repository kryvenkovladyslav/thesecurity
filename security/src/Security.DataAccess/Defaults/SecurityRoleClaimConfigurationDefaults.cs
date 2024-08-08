using Security.Abstract;

namespace Security.DataAccess
{
    /// <summary>
    /// Provides constants for customizing <see cref="SecurityRoleClaim{TIdentifier}"/> table
    /// </summary>
    internal static class SecurityRoleClaimConfigurationDefaults
    {
        /// <summary>
        /// Provides a name of the table
        /// </summary>
        public static string TableName { get; } = "SecurityRoleClaim";

        /// <summary>
        /// Provides the name of the column representing the primary key
        /// </summary>
        public static string IdentifierColumnName { get; } = "ID";

        /// <summary>
        /// Provides the name of the column representing the foreign key
        /// </summary>
        public static string RoleIdentifierColumnName { get; } = "RoleID";

        /// <summary>
        /// Provides the name of the column representing the claim type
        /// </summary>
        public static string ClaimTypeColumnName { get; } = "Type";

        /// <summary>
        /// Provides the name of the column representing the claim value
        /// </summary>
        public static string ClaimValueColumnName { get; } = "Value";
    }
}