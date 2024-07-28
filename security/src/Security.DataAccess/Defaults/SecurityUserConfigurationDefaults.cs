using Security.Abstract;

namespace Security.DataAccess
{
    /// <summary>
    /// Provides constants for customizing <see cref="SecurityUser{TIdentifier}"/> table
    /// </summary>
    internal static class SecurityUserConfigurationDefaults
    {
        /// <summary>
        /// Provides a name of the table
        /// </summary>
        public static string TableName { get; } = "SecurityUser";

        /// <summary>
        /// Provides the name of the column representing the primary key
        /// </summary>
        public static string IdentifierColumnName { get; } = "ID";

        /// <summary>
        /// Provides the name of the column representing the name of a user
        /// </summary>
        public static string UserNameColumnName { get; } = "UserName";

        /// <summary>
        /// Provides the name of the column representing the phone number
        /// </summary>
        public static string PhoneNumberColumnName { get; } = "PhoneNumber";

        /// <summary>
        /// Provides the name of the column representing the confirmed phone number
        /// </summary>
        public static string PhoneNumberConfirmedColumnName { get; } = "PhoneNumberConfirmed";

        /// <summary>
        /// Provides the name of the column representing the email
        /// </summary>
        public static string EmailColumnName { get; } = "Email";

        /// <summary>
        /// Provides the name of the column representing the normalized email
        /// </summary>
        public static string NormalizedEmailColumnName { get; } = "NormalizedEmail";

        /// <summary>
        /// Provides the name of the column representing the confirmed email
        /// </summary>
        public static string EmailConfirmedColumnName { get; } = "EmailConfirmed";

        /// <summary>
        /// Provides the name of the column representing the normalized user name
        /// </summary>
        public static string NormalizedUserNameColumnName { get; } = "NormalizedUserName";

        /// <summary>
        /// Provides the name of the column representing the security stamp
        /// </summary>
        public static string SecurityStampColumnName { get; } = "SecurityStamp";

        /// <summary>
        /// Provides the name of the column representing the hashed password
        /// </summary>
        public static string PasswordHashColumnName { get; set; } = "PasswordHash";
    }
}