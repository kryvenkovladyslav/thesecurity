using Microsoft.EntityFrameworkCore;
using Security.Abstract;
using System;

namespace Security.DataAccess
{
    public abstract class SecurityDatabaseContext<TUser, TRole, TClaim, TUserRole, TRoleClaim, TIdentifier> : DbContext
        where TRole : SecurityRole<TIdentifier>
        where TUser : SecurityUser<TIdentifier>
        where TClaim : SecurityClaim<TIdentifier>
        where TUserRole : SecurityUserRole<TIdentifier>
        where TRoleClaim : SecurityRoleClaim<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        public SecurityDatabaseContext() { }

        protected SecurityDatabaseContextOptions SecurityDatabaseOptions { get; private init; }

        public SecurityDatabaseContext(SecurityDatabaseContextOptions securityDatabaseOptions)
        {
            this.SecurityDatabaseOptions = securityDatabaseOptions ?? throw new ArgumentNullException(nameof(securityDatabaseOptions));
        }

        public DbSet<TUser> Users { get; init; }

        public DbSet<TRole> Roles { get; init; }

        public DbSet<TClaim> Claims { get; init; }

        public DbSet<TUserRole> UserRoles { get; init; }

        public DbSet<TRoleClaim> RoleClaims { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SecurityUserConfiguration<TUser, TIdentifier>());
            modelBuilder.ApplyConfiguration(new SecurityClaimConfiguration<TUser, TClaim, TIdentifier>());
        }
    }
}