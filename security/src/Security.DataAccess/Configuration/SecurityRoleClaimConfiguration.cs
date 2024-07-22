using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    internal sealed class SecurityRoleClaimConfiguration<TSecurityRole,TIdentifier> : IEntityTypeConfiguration<TSecurityRole>
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityRole : SecurityRole<TIdentifier>
    {
        public void Configure(EntityTypeBuilder<TSecurityRole> builder)
        {
            var roleTable = builder.ToTable(SecurityRoleConfigurationDefaults.TableName);

            roleTable.HasKey(role => role.ID);

            roleTable
                .Property(role => role.ID)
                .HasColumnName(SecurityRoleConfigurationDefaults.IdentifierColumnName)
                .IsRequired();

            roleTable
                .Property(role => role.Name)
                .HasColumnName(SecurityRoleConfigurationDefaults.NameColumnName)
                .IsRequired();

            roleTable
                .Property(role => role.NormalizedName)
                .HasColumnName(SecurityRoleConfigurationDefaults.NormalizedNameColumnName)
                .IsRequired();

            roleTable
                .Property(role => role.ConcurrencyStamp)
                .HasColumnName(SecurityRoleConfigurationDefaults.ConcurrencyStampColumnName)
                .IsRequired();
        }
    }
}