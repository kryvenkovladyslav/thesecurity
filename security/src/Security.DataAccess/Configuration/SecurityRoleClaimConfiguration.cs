using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    /// <summary>
    /// Provides methods for configuring the <see cref="SecurityRoleClaim{TIdentifier}"/> entity
    /// </summary>
    /// <typeparam name="TSecurityRole">Represents the <see cref="SecurityRole{TIdentifier}"/></typeparam>
    /// <typeparam name="TSecurityRoleClaim">Represents the <see cref="SecurityRoleClaim{TSecurityRoleClaim}"/></typeparam>
    /// <typeparam name="TIdentifier">Represents an identifier of the <see cref="SecurityRole{TIdentifier}"/></typeparam>
    internal sealed class SecurityRoleClaimConfiguration<TSecurityRole, TSecurityRoleClaim, TIdentifier> : IEntityTypeConfiguration<TSecurityRoleClaim>
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityRole : SecurityRole<TIdentifier>
        where TSecurityRoleClaim : SecurityRoleClaim<TIdentifier>
    {
        /// <summary>
        /// Configures the <see cref="SecurityRoleClaim{TIdentifier}"/> table inside the database
        /// </summary>
        /// <param name="builder">The standard builder for applying table configuration</param>
        public void Configure(EntityTypeBuilder<TSecurityRoleClaim> builder)
        {
            var roleClaimTable = builder.ToTable(SecurityRoleClaimConfigurationDefaults.TableName);

            roleClaimTable.HasKey(roleClaim => roleClaim.ID);

            roleClaimTable
                .Property(roleClaim => roleClaim.ID)
                .HasColumnName(SecurityRoleClaimConfigurationDefaults.IdentifierColumnName)
                .IsRequired();

            roleClaimTable
                .Property(roleClaim => roleClaim.Value)
                .HasColumnName(SecurityRoleClaimConfigurationDefaults.ClaimValueColumnName)
                .IsRequired();

            roleClaimTable
                .Property(roleClaim => roleClaim.Type)
                .HasColumnName(SecurityRoleClaimConfigurationDefaults.ClaimTypeColumnName)
                .IsRequired();

            roleClaimTable
                .Property(roleClaim => roleClaim.RoleID)
                .HasColumnName(SecurityRoleClaimConfigurationDefaults.RoleIdentifierColumnName)
                .IsRequired();

            roleClaimTable.HasOne<TSecurityRole>().WithMany()
                .HasForeignKey(roleClaim => roleClaim.RoleID).HasPrincipalKey(role => role.ID);
        }

    }
}