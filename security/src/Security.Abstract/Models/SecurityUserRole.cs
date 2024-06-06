using System;
using System.Text;

namespace Security.Abstract
{
    /// <summary>
    /// Represents a current user-role in the Security System
    /// </summary>
    /// <typeparam name="TIdentifier">The type representing an identifier for current entity</typeparam>
    public class SecurityUserRole<TIdentifier> : SecurityEntity<TIdentifier>, IEquatable<SecurityUserRole<TIdentifier>>
        where TIdentifier: IEquatable<TIdentifier>
    {
        /// <summary>
        /// Represents a user identifier
        /// </summary>
        public virtual TIdentifier UserID { get; init; }

        /// <summary>
        /// Represents a role identifier
        /// </summary>
        public virtual TIdentifier RoleID { get; init; }

        /// <summary>
        /// Compares two user-roles by identifier
        /// </summary>
        /// <param name="obj">The user-role will be compared</param>
        /// <returns>True if two object have the same identifier, otherwise False</returns>
        public override bool Equals(object obj)
        {
            var userRole = obj as SecurityUserRole<TIdentifier>;

            return userRole != null ? this.Equals(userRole) : false;
        }

        /// <summary>
        /// Compares two user-roles by identifier
        /// </summary>
        /// <param name="other">The user-role will be compared</param>
        /// <returns>True if two object have the same identifier, otherwise False</returns>
        public bool Equals(SecurityUserRole<TIdentifier> other)
        {
            return this.ID.Equals(other.ID);
        }

        /// <summary>
        /// Returns a hash code of the instance
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        /// <summary>
        /// Provides a string representation for the current user-role
        /// </summary>
        /// <returns>A string representing a user-role state</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder
                .Append("{ID:\t")
                .Append(this.ID)
                .Append(",\t")
                .Append("UserID:\t")
                .Append(this.UserID)
                .Append(",\t")
                .Append("RoleID:\t")
                .Append(this.RoleID)
                .Append("}");

            return builder.ToString();
        }
    }
}