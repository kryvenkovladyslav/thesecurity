using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    /// <summary>
    /// Provides methods for configuring the <see cref="SecurityClaim{TIdentifier}"/> entity
    /// </summary>
    /// <typeparam name="TSecurityUser">Represents the <see cref="SecurityUser{TIdentifier}"/></typeparam>
    /// <typeparam name="TSecurityUserClaim">Represents the <see cref="SecurityClaim{TIdentifier}"/></typeparam>
    /// <typeparam name="TIdentifier">Represents an identifier of the <see cref="SecurityClaim{TIdentifier}"/></typeparam>
    internal sealed class SecurityUserClaimConfiguration<TSecurityUser, TSecurityUserClaim, TIdentifier> : IEntityTypeConfiguration<TSecurityUserClaim>
        where TIdentifier : IEquatable<TIdentifier>
        where TSecurityUser : SecurityUser<TIdentifier>
        where TSecurityUserClaim : SecurityClaim<TIdentifier>
    {
        /// <summary>
        /// Configures the <see cref="SecurityClaim{TIdentifier}"/> table inside the database
        /// </summary>
        /// <param name="builder">The standard builder for applying table configuration</param>
        public void Configure(EntityTypeBuilder<TSecurityUserClaim> builder)
        {
            var securityUserClaimTable = builder.ToTable(SecurityUserClaimConfigurationDefaults.TableName);

            securityUserClaimTable.HasKey(claim => claim.ID);

            securityUserClaimTable
                .Property(claim => claim.ID)
                .HasColumnName(SecurityUserClaimConfigurationDefaults.IdentifierColumnName)
                .IsRequired();

            securityUserClaimTable
                .Property(claim => claim.UserID)
                .HasColumnName(SecurityUserClaimConfigurationDefaults.UserIdentifierColumnName)
                .IsRequired();

            securityUserClaimTable
                .Property(claim => claim.Type)
                .HasColumnName(SecurityUserClaimConfigurationDefaults.TypeColumnName)
                .IsRequired();

            securityUserClaimTable
                .Property(claim => claim.Value)
                .HasColumnName(SecurityUserClaimConfigurationDefaults.ValueColumnName)
                .IsRequired();

            securityUserClaimTable
                .HasOne<TSecurityUser>()
                .WithMany()
                .HasForeignKey(claim => claim.UserID)
                .HasPrincipalKey(user => user.ID);
        }
    }
}