using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    public abstract class SecurityDatabaseContext<TUser, TIdentifier, TClaim> : DbContext
        where TUser : SecurityUser<TIdentifier>
        where TClaim : SecurityClaim<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        protected SecurityDatabaseContextOptions SecurityDatabaseOptions { get; private init; }

        public SecurityDatabaseContext(SecurityDatabaseContextOptions securityDatabaseOptions)
        {
            this.SecurityDatabaseOptions = securityDatabaseOptions ?? throw new ArgumentNullException(nameof(securityDatabaseOptions));
        }

        public DbSet<TUser> Users { get; init; }

        public DbSet<TClaim> Claims { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SecurityUserConfiguration<TUser, TIdentifier>());
            modelBuilder.ApplyConfiguration(new SecurityClaimConfiguration<TClaim, TIdentifier>());
        }
    }
}