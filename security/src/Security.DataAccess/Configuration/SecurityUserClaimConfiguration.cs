using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    internal sealed class SecurityUserClaimConfiguration<TSecurityUser, TSecurityUserClaim, TIdentifier> : IEntityTypeConfiguration<TSecurityUserClaim>
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityUser : SecurityUser<TIdentifier>
        where TSecurityUserClaim : SecurityClaim<TIdentifier>
    {
        public void Configure(EntityTypeBuilder<TSecurityUserClaim> builder)
        {
            var securityUserClaimTable = builder.ToTable(SecurityClaimConfigurationDefaults.SecurityClaimTableName);

            securityUserClaimTable.HasKey(claim => claim.ID);

            securityUserClaimTable
                .Property(claim => claim.ID)
                .HasColumnName(SecurityClaimConfigurationDefaults.IdentifierColumnName)
                .IsRequired();

            securityUserClaimTable
                .Property(claim => claim.UserID)
                .HasColumnName(SecurityClaimConfigurationDefaults.UserIdentifierColumnName)
                .IsRequired();

            securityUserClaimTable
                .Property(claim => claim.Type)
                .HasColumnName(SecurityClaimConfigurationDefaults.TypeColumnName)
                .IsRequired();

            securityUserClaimTable
                .Property(claim => claim.Value)
                .HasColumnName(SecurityClaimConfigurationDefaults.ValueColumnName)
                .IsRequired();

            securityUserClaimTable
                .HasOne<TSecurityUser>()
                .WithMany()
                .HasForeignKey(claim => claim.UserID)
                .HasPrincipalKey(user => user.ID);
        }
    }
}