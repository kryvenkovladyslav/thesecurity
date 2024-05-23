using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    internal class SecurityClaimConfiguration<TClaim, TIdentifier> : IEntityTypeConfiguration<TClaim>
        where TClaim : SecurityClaim<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public void Configure(EntityTypeBuilder<TClaim> builder)
        {
            builder.HasKey(claim => claim.ID);

            builder.Property(claim => claim.ID).IsRequired();
            builder.Property(claim => claim.Type).IsRequired();
            builder.Property(claim => claim.Value).IsRequired();
            builder.Property(claim => claim.UserID).IsRequired();
        }
    }
}