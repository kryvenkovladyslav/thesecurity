using System;
using System.Text;

namespace Security.Abstract
{
    /// <summary>
    /// This class represents a claim in the Security System
    /// </summary>
    /// <typeparam name="TIdentifier">Represents the identifier of a claim</typeparam>
    public class SecurityClaim<TIdentifier> : IEquatable<SecurityClaim<TIdentifier>>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Represent the identifier of a user's claim
        /// </summary>
        public virtual TIdentifier ID { get; init; }
    
        /// <summary>
        /// Represent the identifier of a user
        /// </summary>
        public virtual TIdentifier UserID { get; init; }

        /// <summary>
        /// Represent a type of the claim
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Represent a value of the claim
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Compares two claims by identifier
        /// </summary>
        /// <param name="other">The object will be compared</param>
        /// <returns>True if the object represents a claim with a special identifier, otherwise False</returns>
        public bool Equals(SecurityClaim<TIdentifier> other)
        {
            return this.ID.Equals(other.ID);
        }

        /// <summary>
        /// Compares two claims by identifier
        /// </summary>
        /// <param name="obj">The object will be compared</param>
        /// <returns>True if the object represents a claim with a special identifier, otherwise False</returns>
        public override bool Equals(object obj)
        {
            var claim = obj as SecurityClaim<TIdentifier>;

            return obj as SecurityClaim<TIdentifier> != null ? this.Equals(claim) : false;
        }

        /// <summary>
        /// Returns a hash code of the instance
        /// </summary>
        /// <returns>Generated hash for current claim</returns>
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        /// <summary>
        /// Provides a string representation for the current user's claim
        /// </summary>
        /// <returns>A string representing a claim state</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder
                .Append("{ClaimID:\t")
                .Append(this.ID)
                .Append(",\t")
                .Append("Type:\t")
                .Append(this.Type)
                .Append(",\t")
                .Append("Value:\t")
                .Append(this.Value)
                .Append("}");

            return builder.ToString();
        }
    }
}