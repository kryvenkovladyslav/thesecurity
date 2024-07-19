using System;

namespace Security.Abstract
{
    /// <summary>
    /// Represents a base entity stored in database
    /// </summary>
    /// <typeparam name="TIdentifier">The type representing an identifier for current entity</typeparam>
    public abstract class SecurityEntity<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier> 
    {
        /// <summary>
        /// Represent the identifier of then entity
        /// </summary>
        public virtual TIdentifier ID { get; init; }
    }
}