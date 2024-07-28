using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Abstract;
using System;

namespace Security.DataAccess.Configuration
{
    /// <summary>
    /// Provides methods for configuring the <see cref="SecurityUserRole{TIdentifier}"/> entity
    /// </summary>
    /// <typeparam name="TSecurityUser">Represents the <see cref="SecurityUser{TIdentifier}"/></typeparam>
    /// <typeparam name="TSecurityRole">Represents the <see cref="SecurityRole{TIdentifier}"/></typeparam>
    /// <typeparam name="TSecurityUserRole">Represents the <see cref="SecurityUserRole{TIdentifier}"/></typeparam>
    /// <typeparam name="TIdentifier">Represents an identifier of the <see cref="SecurityUserRole{TIdentifier}"/></typeparam>
    internal sealed class SecurityUserRoleConfiguration<TSecurityUser, TSecurityRole, TSecurityUserRole, TIdentifier> : IEntityTypeConfiguration<TSecurityUserRole>
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityUser : SecurityUser<TIdentifier>
        where TSecurityRole : SecurityRole<TIdentifier>
        where TSecurityUserRole : SecurityUserRole<TIdentifier>
    {
        /// <summary>
        /// Configures the <see cref="SecurityUserRole{TIdentifier}"/> table inside the database
        /// </summary>
        /// <param name="builder">The standard builder for applying table configuration</param>
        public void Configure(EntityTypeBuilder<TSecurityUserRole> builder)
        {
            var securityUserRoleTable = builder.ToTable(SecurityUserRoleConfigurationDefaults.TableName);

            securityUserRoleTable.HasKey(claim => claim.ID);

            securityUserRoleTable
                .Property(userRole => userRole.ID)
                .HasColumnName(SecurityUserRoleConfigurationDefaults.IdentifierColumnName)
                .IsRequired();

            securityUserRoleTable
                .Property(userRole => userRole.UserID)
                .HasColumnName(SecurityUserRoleConfigurationDefaults.UserIdentifierColumnName)
                .IsRequired();

            securityUserRoleTable
                .Property(userRole => userRole.RoleID)
                .HasColumnName(SecurityUserRoleConfigurationDefaults.RoleIdentifierColumnName)
                .IsRequired();

            securityUserRoleTable.HasOne<TSecurityUser>().WithMany()
                .HasForeignKey(userRole => userRole.UserID).HasPrincipalKey(user => user.ID);

            securityUserRoleTable.HasOne<TSecurityRole>().WithMany()
                .HasForeignKey(userRole => userRole.RoleID).HasPrincipalKey(role => role.ID);
        }
    }
}