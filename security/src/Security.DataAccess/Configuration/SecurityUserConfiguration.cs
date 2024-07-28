using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    /// <summary>
    /// Provides methods for configuring the <see cref="SecurityUser{TIdentifier}"/> entity
    /// </summary>
    /// <typeparam name="TSecurityUser">Represents the <see cref="SecurityUser{TIdentifier}"/></typeparam>
    /// <typeparam name="TIdentifier">Represents an identifier of the <see cref="SecurityUser{TIdentifier}"/></typeparam>
    internal sealed class SecurityUserConfiguration<TSecurityUser, TIdentifier> : IEntityTypeConfiguration<TSecurityUser>
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityUser : SecurityUser<TIdentifier>
    {
        /// <summary>
        /// Configures the <see cref="SecurityUser{TIdentifier}"/> table inside the database
        /// </summary>
        /// <param name="builder">The standard builder for applying table configuration</param>
        public void Configure(EntityTypeBuilder<TSecurityUser> builder)
        {
            var securityUserTable = builder.ToTable(SecurityUserConfigurationDefaults.TableName);

            securityUserTable.HasKey(user => user.ID);

            securityUserTable
                .Property(user => user.ID)
                .HasColumnName(SecurityUserConfigurationDefaults.IdentifierColumnName)
                .IsRequired();

            securityUserTable
                .Property(user => user.UserName)
                .HasColumnName(SecurityUserConfigurationDefaults.UserNameColumnName)
                .IsRequired();

            securityUserTable
                .Property(user => user.Email)
                .HasColumnName(SecurityUserConfigurationDefaults.EmailColumnName)
                .IsRequired();

            securityUserTable
                .Property(user => user.PhoneNumber)
                .HasColumnName(SecurityUserConfigurationDefaults.PhoneNumberColumnName)
                .HasDefaultValue(null)
                .IsRequired();

            securityUserTable.Property(user => user.IsEmailConfirmed)
                .HasColumnName(SecurityUserConfigurationDefaults.EmailConfirmedColumnName)
                .HasDefaultValue(false)
                .IsRequired();

            securityUserTable
                .Property(user => user.IsPhoneNumberConfirmed)
                .HasColumnName(SecurityUserConfigurationDefaults.PhoneNumberConfirmedColumnName)
                .HasDefaultValue(false)
                .IsRequired();

            securityUserTable
                .Property(user => user.NormalizedEmail)
                .HasColumnName(SecurityUserConfigurationDefaults.NormalizedEmailColumnName)
                .IsRequired();

            securityUserTable
                .Property(user => user.NormalizedUserName)
                .HasColumnName(SecurityUserConfigurationDefaults.NormalizedUserNameColumnName)
                .IsRequired();

            securityUserTable
                .Property(user => user.SecurityStamp)
                .HasColumnName(SecurityUserConfigurationDefaults.SecurityStampColumnName)
                .HasDefaultValue(null)
                .IsRequired(false);

            securityUserTable
                .Property(user => user.PasswordHash)
                .HasColumnName(SecurityUserConfigurationDefaults.PasswordHashColumnName)
                .IsRequired();
        }
    }
}