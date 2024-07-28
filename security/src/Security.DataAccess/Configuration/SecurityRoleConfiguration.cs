using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    internal sealed class SecurityRoleConfiguration<TSecurityRole, TIdentifier> : IEntityTypeConfiguration<TSecurityRole>
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityRole : SecurityRole<TIdentifier>
    {
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