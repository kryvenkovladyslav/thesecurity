using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    internal sealed class SecurityClaimConfiguration<TUser, TClaim, TIdentifier> : IEntityTypeConfiguration<TClaim>
        where TUser : SecurityUser<TIdentifier>
        where TClaim : SecurityClaim<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public void Configure(EntityTypeBuilder<TClaim> builder)
        {
            var claimTable = builder.ToTable(SecurityClaimConfigurationDefaults.SecurityClaimTableName);

            claimTable.HasKey(claim => claim.ID);

            claimTable
                .Property(claim => claim.ID)
                .HasColumnName(SecurityClaimConfigurationDefaults.IdentifierColumnName)
                .IsRequired();

            claimTable
                .Property(claim => claim.UserID)
                .HasColumnName(SecurityClaimConfigurationDefaults.UserIdentifierColumnName)
                .IsRequired();

            claimTable
                .Property(claim => claim.Type)
                .HasColumnName(SecurityClaimConfigurationDefaults.TypeColumnName)
                .IsRequired();

            claimTable
                .Property(claim => claim.Value)
                .HasColumnName(SecurityClaimConfigurationDefaults.ValueColumnName)
                .IsRequired();

            claimTable
                .HasOne<TUser>()
                .WithMany()
                .HasForeignKey(claim => claim.UserID)
                .HasPrincipalKey(user => user.ID);
        }
    }
}