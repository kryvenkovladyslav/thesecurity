using Security.Abstract;

namespace Security.DataAccess
{
    /// <summary>
    /// Provides constants for customizing <see cref="SecurityRole{TIdentifier}"/> table
    /// </summary>
    internal static class SecurityRoleConfigurationDefaults
    {
        /// <summary>
        /// Provides a name of the table
        /// </summary>
        public static string TableName { get; } = "SecurityRole";

        /// <summary>
        /// Provides the name of the column representing the primary key
        /// </summary>
        public static string IdentifierColumnName { get; } = "ID";

        /// <summary>
        /// Provides the name of the column representing the role name
        /// </summary>
        public static string NameColumnName { get; } = "Name";

        /// <summary>
        /// Provides the name of the column representing the normalized role name
        /// </summary>
        public static string NormalizedNameColumnName { get; } = "NormalizedName";

        /// <summary>
        /// Provides the name of the column representing the concurrency stamp
        /// </summary>
        public static string ConcurrencyStampColumnName { get; } = "ConcurrencyStamp";
    }
}