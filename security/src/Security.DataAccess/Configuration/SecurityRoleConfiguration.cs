using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    /// <summary>
    /// Provides methods for configuring the <see cref="SecurityRole{TIdentifier}"/> entity
    /// </summary>
    /// <typeparam name="TSecurityRole">Represents the <see cref="SecurityRole{TIdentifier}"/></typeparam>
    /// <typeparam name="TIdentifier">Represents an identifier of the <see cref="SecurityRole{TIdentifier}"/></typeparam>
    internal sealed class SecurityRoleConfiguration<TSecurityRole, TIdentifier> : IEntityTypeConfiguration<TSecurityRole>
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityRole : SecurityRole<TIdentifier>
    {
        /// <summary>
        /// Configures the <see cref="SecurityRole{TIdentifier}"/> table inside the database
        /// </summary>
        /// <param name="builder">The standard builder for applying table configuration</param>
        public void Configure(EntityTypeBuilder<TSecurityRole> builder)
        {
            var securityRoleTable = builder.ToTable(SecurityRoleConfigurationDefaults.TableName);

            securityRoleTable.HasKey(role => role.ID);

            securityRoleTable
                .Property(role => role.ID)
                .HasColumnName(SecurityRoleConfigurationDefaults.IdentifierColumnName)
                .IsRequired();

            securityRoleTable
                .Property(role => role.Name)
                .HasColumnName(SecurityRoleConfigurationDefaults.NameColumnName)
                .IsRequired();

            securityRoleTable
                .Property(role => role.NormalizedName)
                .HasColumnName(SecurityRoleConfigurationDefaults.NormalizedNameColumnName)
                .IsRequired();

            securityRoleTable
                .Property(role => role.ConcurrencyStamp)
                .HasColumnName(SecurityRoleConfigurationDefaults.ConcurrencyStampColumnName)
                .IsRequired();
        }
    }
}