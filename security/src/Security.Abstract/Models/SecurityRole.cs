using System;

namespace Security.Abstract
{
    /// <summary>
    /// Represents a role in the Security System
    /// </summary>
    /// <typeparam name="TIdentifier">The type representing an identifier for current entity</typeparam>
    public class SecurityRole<TIdentifier> : SecurityEntity<TIdentifier>, IEquatable<SecurityRole<TIdentifier>>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// The name of current role
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The normalized name of current entity
        /// </summary>
        public virtual string NormalizedName { get; set; }

        /// <summary>
        /// The ConcurrencyStamp of current entity
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; }

        /// <summary>
        /// The default constructor is used to create an Identity Role
        /// </summary>
        public SecurityRole() { }

        /// <summary>
        /// The constructor is used to create an Identity role with a name
        /// </summary>
        /// <param name="roleName">The name of current role</param>
        /// <exception cref="ArgumentNullException">throws is a name of the role is null</exception>
        public SecurityRole(string roleName)
        {
            this.Name = roleName ?? throw new ArgumentNullException(nameof(roleName));
        }

        /// <summary>
        /// Compares two roles by identifier
        /// </summary>
        /// <param name="obj">The object will be compared</param>
        /// <returns>True if the object represents a role with a special identifier, otherwise False</returns>
        public override bool Equals(object obj)
        {
            var role = obj as SecurityRole<TIdentifier>;

            return role != null ? this.Equals(role) : false;
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
        /// Compares two roles by identifier
        /// </summary>
        /// <param name="other">The role will be compared</param>
        /// <returns>True if two object have the same identifier, otherwise False</returns>
        public bool Equals(SecurityRole<TIdentifier> other)
        {
            return this.ID.Equals(other.ID) && this.NormalizedName == other.NormalizedName;
        }

        /// <summary>
        /// Overridden method for returning a name of current role
        /// </summary>
        /// <returns>The name of current role</returns>
        public override string ToString()
        {
            return this.Name ?? string.Empty;
        }
    }
}