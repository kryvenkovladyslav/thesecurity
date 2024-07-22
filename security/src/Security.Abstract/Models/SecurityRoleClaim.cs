using System;
using System.Text;

namespace Security.Abstract
{
    /// <summary>
    /// Represents a claim that is granted to all users within a role
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the primary key of the role associated with this claim</typeparam>
    public class SecurityRoleClaim<TIdentifier> : SecurityEntity<TIdentifier>, IEquatable<SecurityRoleClaim<TIdentifier>>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Gets the primary key of the role
        /// </summary>
        public virtual TIdentifier RoleID { get; init; }

        /// <summary>
        /// Gets the primary key of the claim 
        /// </summary>
        public virtual TIdentifier ClaimID { get; init; }

        /// <summary>
        /// Compares two role-claim by identifier
        /// </summary>
        /// <param name="obj">The object will be compared</param>
        /// <returns>True if the object represents a role with a special identifier, otherwise False</returns>
        public override bool Equals(object obj)
        {
            var roleClaim = obj as SecurityRoleClaim<TIdentifier>;

            return roleClaim != null ? this.Equals(roleClaim) : false;
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
        /// Compares two role-claim by identifier
        /// </summary>
        /// <param name="other">The role will be compared</param>
        /// <returns>True if two object have the same identifier, otherwise False</returns>
        public bool Equals(SecurityRoleClaim<TIdentifier> other)
        {
            return this.ID.Equals(other.ID);
        }

        /// <summary>
        /// Overridden method for returning a name of current role-claim
        /// </summary>
        /// <returns>The name of current role-claim</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder
                .Append("{ClaimID:\t")
                .Append(this.ClaimID)
                .Append(",\t")
                .Append("{RoleID:\t")
                .Append(this.RoleID)
                .Append("}");

            return builder.ToString();
        }
    }
}