using Security.Abstract;

namespace Security.DataAccess
{
    /// <summary>
    /// Provides constants for customizing <see cref="SecurityClaim{TIdentifier}"/> table
    /// </summary>
    internal static class SecurityUserClaimConfigurationDefaults
    {
        /// <summary>
        /// Provides a name of the table
        /// </summary>
        public static string TableName { get; } = "SecurityUserClaim";

        /// <summary>
        /// Provides the name of the column representing the primary key
        /// </summary>
        public static string IdentifierColumnName { get; } = "ID";

        /// <summary>
        /// Provides the name of the column representing the foreign key
        /// </summary>
        public static string UserIdentifierColumnName { get; } = "UserID";

        /// <summary>
        /// Provides the name of the column representing the claim type
        /// </summary>
        public static string TypeColumnName { get; } = "Type";

        /// <summary>
        /// Provides the name of the column representing the claim value
        /// </summary>
        public static string ValueColumnName { get; } = "Value";
    }
}