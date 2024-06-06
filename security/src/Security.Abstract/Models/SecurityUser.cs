﻿using System;
using System.Text;

namespace Security.Abstract
{
    /// <summary>
    /// This class represents a user in the Security System
    /// </summary>
    /// <typeparam name="TIdentifier">Represents the identifier of a user</typeparam>
    public abstract class SecurityUser<TIdentifier> : SecurityEntity<TIdentifier>, IEquatable<SecurityUser<TIdentifier>>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Represents a name of a user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Represents a phone number of a user
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Indicates if the phone number confirmed by a user
        /// </summary>
        public bool IsPhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Represents an email of a user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Represents a normalized email of a user
        /// </summary>
        public string NormalizedEmail { get; set; }

        /// <summary>
        /// Indicates if the email number confirmed by a user
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Represents a normalized name of a user
        /// </summary>
        public string NormalizedUserName { get; set; }

        /// <summary>
        /// Compares two users by identifier
        /// </summary>
        /// <param name="obj">The object will be compared</param>
        /// <returns>True if the object represents a user with a special identifier, otherwise False</returns>
        public override bool Equals(object obj)
        {
            var user = obj as SecurityUser<TIdentifier>;

            return user != null ? this.Equals(user) : false;
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
        /// Compares two users by identifier
        /// </summary>
        /// <param name="other">The user will be compared</param>
        /// <returns>True if two object have the same identifier, otherwise False</returns>
        public bool Equals(SecurityUser<TIdentifier> other)
        {
            return this.ID.Equals(other.ID) && this.NormalizedUserName == other.NormalizedUserName;
        }

        /// <summary>
        /// Provides a string representation for the current user
        /// </summary>
        /// <returns>A string representing a user state</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder
                .Append("{UserID:\t")
                .Append(this.ID)
                .Append(",\t")
                .Append("UserName:\t")
                .Append(this.UserName)
                .Append("}");

            return builder.ToString();
        }
    }
}